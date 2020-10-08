using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.Business;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Extensions;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel: IBudgetRequestListViewModel
    {
        public event System.Action ViewModelChanged;

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

        private IBudgetRequestListServiceFactory _budgetRequestListServiceFactory;
        private IAgreementBaseService _agreementBaseService;
        private IBudgetRequestService _budgetRequestService;
        private int _pageNumber;

        public IFilterViewModel FilterViewModel { get; set; }
        public IBudgetRequestViewModel BudgetRequestViewModel { get; set; }
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
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<BudgetRequestRowViewModel> BudgetRequests { get; }

        public List<BudgetRequestRowViewModel> DisplayBudgetRequests { get; }

        public List<BudgetRequestRowViewModel> BudgetRequestPage
        {
            get
            {
                FilterCustomization();
                List<BudgetRequestRowViewModel> list = DisplayBudgetRequests
                    .RequesterFilter(FilterViewModel.Requester)
                    .DepartmentFilter(FilterViewModel.Department)
                    .UnitFilter(FilterViewModel.Unit)
                    .StateFilter(FilterViewModel.State)
                    .DateFilter(FilterViewModel.From, FilterViewModel.To)
                    .ToList();

                Total = list.Count;
                if (list.Count - PageSize * (PageNumber - 1) > 0)
                {
                    return list.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
                }
                else
                {
                    return list.Skip(PageSize * (PageNumber - 1)).Take(list.Count - PageSize * PageNumber).ToList();
                }
            }
        }

        public void ApplySelection()
        {
            if (FilterViewModel.SelectedFilterIsActive)
            {
                List<BudgetRequestRowViewModel> list = DisplayBudgetRequests.SelectedRowsFilter().ToList();
                DisplayBudgetRequests.Clear();
                DisplayBudgetRequests.AddRange(list);
            }
            else
            {
                DisplayBudgetRequests.Clear();
                DisplayBudgetRequests.AddRange(BudgetRequests);
            }
            
        }

        public void OnGoingList()
        {
            if (FilterViewModel.OnGoingFilterIsActive)
            {
                List<BudgetRequestRowViewModel> list = DisplayBudgetRequests.OnGoingFilter(role, userInfo).ToList();
                DisplayBudgetRequests.Clear();
                DisplayBudgetRequests.AddRange(list);

            }
            else
            {
                DisplayBudgetRequests.Clear();
                DisplayBudgetRequests.AddRange(BudgetRequests);
            }
            
        }

        public BudgetRequestListViewModel(IBudgetRequestListServiceFactory budgetRequestListServiceFactory, IAgreementBaseService agreementBaseService,IBudgetRequestService budgetRequestService)
        {
            BudgetRequests = new List<BudgetRequestRowViewModel>();
            DisplayBudgetRequests = new List<BudgetRequestRowViewModel>();
            FilterViewModel = new FilterViewModel();
            _budgetRequestListServiceFactory = budgetRequestListServiceFactory;
            _agreementBaseService = agreementBaseService;
            _budgetRequestService = budgetRequestService;
            PageNumber = 1;
            PageSize = 10;
            
        }

        public void LoadData()
        {
            IBudgetRequestListService budgetRequestListService = _budgetRequestListServiceFactory.Create(role);
            foreach (BudgetRequestMainListDto request in budgetRequestListService.GetList(userInfo))
            {
                BudgetRequests.Add(new BudgetRequestRowViewModel(request));
            }
            DisplayBudgetRequests.Clear();
            DisplayBudgetRequests.AddRange(BudgetRequests);
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
            BudgetRequestUpdateOutput output = _agreementBaseService.ApproveListByRole(ids, role);

            foreach (BudgetRequestMainListDto request in output.SuccessUpdatedBudgetRequests)
            {
                
                for (int i = 0; i < BudgetRequests.Count; i++)
                {
                    if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                    {
                        BudgetRequests[i]=new BudgetRequestRowViewModel(request);
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

        private void FilterCustomization()
        {
            FilterViewModel.RequesterIds.AddRange(DisplayBudgetRequests.Select(br=>br.BudgetRequest.RequesterId).ToList().Distinct());
            FilterViewModel.States.AddRange(DisplayBudgetRequests.Select(br => br.BudgetRequest.State).ToList().Distinct());
            FilterViewModel.DepartmentIds.AddRange(DisplayBudgetRequests.Select(br => br.BudgetRequest.DepartmentId).ToList().Distinct());
            FilterViewModel.UnitIds.AddRange(DisplayBudgetRequests.Select(br => br.BudgetRequest.UnitId).ToList().Distinct());
            
        }

        public void OpenBudgetRequest(Guid id)
        {
            BudgetRequestViewModel = new BudgetRequestViewModel(id, budgetRequestService:);
        }
    }
}
