﻿using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Localization;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Enums;
using EasyBudget.Presentation.Extensions;
using EasyBudget.Presentation.Interfaces;
using EasyBudget.Presentation.Utils;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel: IBudgetRequestListViewModel
    {
        private readonly IBudgetRequestListServiceFactory _budgetRequestListServiceFactory;
        private readonly IAgreementBaseService _agreementBaseService;
        private readonly IBudgetRequestEntityFactory _requestEntityFactory;
        private readonly IAgreementServiceFactory _agreementServiceFactory;
        

        private int _pageNumber;
        private int _pageSize;
        private List<BudgetRequestRowViewModel> _displayBudgetRequests;

        private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("3148ce2c-540e-4cc4-a372-42e0c29a478b"),
            CurrentRoleId = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            CurrentRoleName = "Director",
        };
        
        private Role role = new Role()
        {
            Department = null,
            Id = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            Name = "Director",
        };

        public BudgetRequestListViewModel(
            IBudgetRequestListServiceFactory budgetRequestListServiceFactory,
            IAgreementBaseService agreementBaseService,
            IBudgetRequestEntityFactory requestEntityFactory,
            IAgreementServiceFactory agreementServiceFactory)
        {
            _budgetRequestListServiceFactory = budgetRequestListServiceFactory;
            _agreementBaseService = agreementBaseService;
            _requestEntityFactory = requestEntityFactory;
            _agreementServiceFactory = agreementServiceFactory;

            BudgetRequests = new List<BudgetRequestRowViewModel>();
            _displayBudgetRequests = new List<BudgetRequestRowViewModel>();
            FilterViewModel = new FilterViewModel();
            Sorting = new SortingList();

            PageNumber = 1;
            PageSize = 10;
            Total = 0;
        }

        public event System.Action ViewModelChanged;

        public IFilterViewModel FilterViewModel { get; }
        public SortingList Sorting { get; set; }

        public IBudgetRequestViewModel BudgetRequestViewModel { get; private set; }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (_pageNumber == value)
                    return;
                _pageNumber = value;
                ViewModelChanged?.Invoke();
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize == value)
                    return;
                _pageSize = value;
                ViewModelChanged?.Invoke();
            }
        }

        public int Total { get; private set; }

        public List<BudgetRequestRowViewModel> BudgetRequests { get; }

        public List<BudgetRequestRowViewModel> PageBudgetRequests
        {
            get
            {
                FilterCustomization();

                List<BudgetRequestRowViewModel> list = _displayBudgetRequests
                    .RequesterFilter(FilterViewModel.Requester)
                    .DepartmentFilter(FilterViewModel.Department)
                    .UnitFilter(FilterViewModel.Unit)
                    .StateFilter(FilterViewModel.State)
                    .DateFilter(FilterViewModel.From, FilterViewModel.To)
                    .SortingList(Sorting)
                    .ToList();

                Total = list.Count;
                if (list.Count - PageSize * (PageNumber - 1) > 0)
                {
                    return list.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
                }

                return list.Skip(PageSize * (PageNumber - 1)).Take(list.Count - PageSize * PageNumber).ToList();
            }
        }

        public void LoadData()
        {
            IBudgetRequestListService budgetRequestListService = _budgetRequestListServiceFactory.Create(role);
            foreach (BudgetRequestMainListDto request in budgetRequestListService.GetList(userInfo))
            {
                BudgetRequests.Add(new BudgetRequestRowViewModel(request));
            }
            _displayBudgetRequests.Clear();
            _displayBudgetRequests.AddRange(BudgetRequests);
            
            FilterCustomization();
            Total = BudgetRequests.Count;
            FilterViewModel.From = BudgetRequests.Select(br => br.BudgetRequest.DateRequested).ToList().Min();
        }

        public void ApproveRequests()
        {
            List<Guid> ids = new List<Guid>();
            foreach (BudgetRequestRowViewModel row in BudgetRequests)
            {
                if (row.IsApproveable && row.IsSelected)
                {
                    ids.Add(row.BudgetRequest.Id);
                }
            }

            if (ids.Count == 0)
            {
                return;
            }

            BudgetRequestListUpdateOutput output = _agreementBaseService.ApproveListByRole(userInfo.Id,ids, role);

            foreach (BudgetRequestMainListDto request in output.SuccessUpdatedBudgetRequests)
            {

                for (int i = 0; i < BudgetRequests.Count; i++)
                {
                    if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                    {
                        BudgetRequests[i] = new BudgetRequestRowViewModel(request);
                        break;
                    }
                }
            }

            foreach (BudgetRequestMainListDto request in output.FailedUpdatedBudgetRequests)
            {
                for (int i = 0; i < BudgetRequests.Count; i++)
                {
                    if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                    {
                        BudgetRequests[i] = new BudgetRequestRowViewModel(request);
                        break;
                    }
                }
            }

        }

        public void ApplySelection()
        {
            if (FilterViewModel.SelectedFilterIsActive)
            {
                List<BudgetRequestRowViewModel> list = _displayBudgetRequests.SelectedRowsFilter().ToList();
                _displayBudgetRequests.Clear();
                _displayBudgetRequests.AddRange(list);
            }
            else
            {
                _displayBudgetRequests.Clear();
                _displayBudgetRequests.AddRange(BudgetRequests);
            }
        }

        public void OnGoingList()
        {
            if (FilterViewModel.OnGoingFilterIsActive)
            {
                List<BudgetRequestRowViewModel> list = _displayBudgetRequests.OnGoingFilter(role, userInfo).ToList();
                _displayBudgetRequests.Clear();
                _displayBudgetRequests.AddRange(list);

            }
            else
            {
                _displayBudgetRequests.Clear();
                _displayBudgetRequests.AddRange(BudgetRequests);
            }
            
        }

        public void OpenBudgetRequest(Guid id)
        {
            BudgetRequestViewModel = _requestEntityFactory.GetExistedRequestViewModel(id);
            ViewModelChanged?.Invoke();
        }

        public void NewBudgetRequest()
        {
            BudgetRequestViewModel = _requestEntityFactory.GetNewRequestViewModel();
            ViewModelChanged?.Invoke();
        }

        public void ReturnToList()
        {
            BudgetRequestViewModel = null;
            ViewModelChanged?.Invoke();
        }

        public void ChangeSorting(SortingEntity entity, bool direction)
        {
            Sorting.Entity = entity;
            Sorting.Direction = direction;
        }

        public void ApproveCertainRequest(BudgetRequestRowViewModel request)
        {
            BudgetRequestUpdateOutput output;
            switch (role.Name)
            {

                case RoleNames.Approver:
                    output = _agreementServiceFactory.GetFirstLine().ApproveFirstLine(userInfo.Id,request.BudgetRequest.Id);
                    UpdateRowsAfterApprove(output);
                    break;
                case RoleNames.Director:
                    output = _agreementServiceFactory.GetDirector().ApproveDirector(userInfo.Id, request.BudgetRequest.Id);
                    UpdateRowsAfterApprove(output);
                    break;
                case RoleNames.FinDirector:
                    output = _agreementServiceFactory.GetFinDirector().ExecutionStartedFinDirector(userInfo.Id, request.BudgetRequest.Id, null);
                    UpdateRowsAfterApprove(output);
                    break;
                case RoleNames.Executor:
                    if (request.BudgetRequest.RealPrice != null)
                    {
                        output = _agreementServiceFactory.GetExecutor().RealPriceAdded(userInfo.Id, request.BudgetRequest.Id, request.BudgetRequest.RealPrice);
                        UpdateRowsAfterApprove(output);
                    }

                    break;
            }
        }

        private void FilterCustomization()
        {
            FilterViewModel.Requesters.Clear();
            FilterViewModel.Requesters
                .AddRange(_displayBudgetRequests
                    .Select(br => new PairGuid(br.BudgetRequest.RequesterId,br.BudgetRequest.RequesterName))
                    .ToList()
                    .Distinct());
            FilterViewModel.Departments.Clear();
            FilterViewModel.Departments
                .AddRange(_displayBudgetRequests
                    .Select(br => new PairGuid(br.BudgetRequest.DepartmentId, br.BudgetRequest.DepartmentName))
                    .ToList()
                    .Distinct());
            FilterViewModel.Units.Clear();
            FilterViewModel.Units
                .AddRange(_displayBudgetRequests
                    .Select(br => new PairGuid(br.BudgetRequest.UnitId, br.BudgetRequest.UnitName))
                    .ToList()
                    .Distinct());
            FilterViewModel.States.Clear();
            FilterViewModel.States
                .AddRange(_displayBudgetRequests
                    .Select(br => new PairEnum<BudgetState>(br.BudgetRequest.State, br.BudgetRequest.State.GetLocalizationState()))
                    .ToList()
                    .Distinct());
        }

        private void UpdateRowsAfterApprove(BudgetRequestUpdateOutput output)
        {
            BudgetRequestMainListDto requestDto = new BudgetRequestMainListDto()
            {
                Id = output.Request.Id,
                Name = output.Request.Name,
                DepartmentId = output.Request.Department.Id,
                DepartmentName = output.Request.Department.Name,
                UnitId = output.Request.Unit.Id,
                UnitName = output.Request.Unit.Name,
                State = output.Request.State,
                DateRequested = output.Request.DateRequested,
                RequesterId = output.Request.Requester.Id,
                RequesterName = output.Request.Requester.Name,
                RealPrice = output.Request.RealPrice
            };
            for (int i = 0; i < BudgetRequests.Count; i++)
            {
                if (output.Request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                {
                    BudgetRequests[i] = new BudgetRequestRowViewModel(requestDto);
                    break;
                }
            }
            for (int i = 0; i < _displayBudgetRequests.Count; i++)
            {
                if (output.Request.Id.Equals(_displayBudgetRequests[i].BudgetRequest.Id))
                {
                    _displayBudgetRequests[i] = new BudgetRequestRowViewModel(requestDto);
                    break;
                }
            }
        }
    }
}
