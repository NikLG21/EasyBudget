using System;
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
            Id = Guid.Parse("6a875efe-05ef-4137-889a-137df8c67ab2"),
            Name = "Кирил Цибулькин",
            CurrentRoleId = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            CurrentRoleName = "Approver",
            UnitId = Guid.Parse("35f0579d-a8d6-4c6a-a241-2f4726b6a9d1"),
            UnitName = "2 Клініка"
        };

        private Role role = new Role()
        {
            Id = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            Name = "Approver",
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
        public event System.Action ComponentChanged;

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
            ComponentChanged?.Invoke();
        }

        public void NewBudgetRequest()
        {
            BudgetRequestViewModel = _requestEntityFactory.GetNewRequestViewModel();
            ComponentChanged?.Invoke();
        }

        public void ReturnToList()
        {
            for (int i = 0; i < BudgetRequests.Count; i++)
            {
                if (BudgetRequests[i].BudgetRequest.Id.Equals(BudgetRequestViewModel.BudgetRequest.Id))
                {
                    BudgetRequests[i] = new BudgetRequestRowViewModel(new BudgetRequestMainListDto()
                    {
                        Id = BudgetRequestViewModel.BudgetRequest.Id,
                        DateRequested = BudgetRequestViewModel.BudgetRequest.DateRequested,
                        DepartmentId = BudgetRequestViewModel.BudgetRequest.DepartmentId,
                        DepartmentName = BudgetRequestViewModel.BudgetRequest.Department.Name,
                        Name = BudgetRequestViewModel.BudgetRequest.Name,
                        RealPrice = BudgetRequestViewModel.BudgetRequest.RealPrice,
                        RequesterId = BudgetRequestViewModel.BudgetRequest.RequesterId,
                        RequesterName = BudgetRequestViewModel.BudgetRequest.Requester.Name,
                        State = BudgetRequestViewModel.BudgetRequest.State,
                        UnitId = BudgetRequestViewModel.BudgetRequest.UnitId,
                        UnitName = BudgetRequestViewModel.BudgetRequest.Unit.Name
                    });
                    break;
                }
            }
            _displayBudgetRequests.Clear();
            _displayBudgetRequests.AddRange(BudgetRequests);
            FilterCustomization();
            FilterViewModel.OnGoingFilterIsActive = false;
            FilterViewModel.SelectedFilterIsActive = false;
            BudgetRequestViewModel = null;
            ComponentChanged?.Invoke();
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
                    output = _agreementServiceFactory.GetFirstLine().ApproveByFirstLine(userInfo,request.BudgetRequest.Id);
                    UpdateRowsAfterApprove(output);
                    break;
                case RoleNames.Director:
                    output = _agreementServiceFactory.GetDirector().ApproveByDirector(userInfo.Id, request.BudgetRequest.Id);
                    UpdateRowsAfterApprove(output);
                    break;
                case RoleNames.FinDirector:
                    output = _agreementServiceFactory.GetFinDirector().ExecutionStartedByFinDirector(userInfo.Id, request.BudgetRequest.Id, null);
                    UpdateRowsAfterApprove(output);
                    break;
                case RoleNames.Executor:
                    if (request.BudgetRequest.RealPrice != null)
                    {
                        output = _agreementServiceFactory.GetExecutor().AddRealPrice(userInfo, request.BudgetRequest.Id, request.BudgetRequest.RealPrice);
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
