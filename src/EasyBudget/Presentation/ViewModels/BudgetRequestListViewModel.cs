using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Extensions;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel: IBudgetRequestListViewModel
    {
        private readonly IBudgetRequestListServiceFactory _budgetRequestListServiceFactory;
        private readonly IAgreementBaseService _agreementBaseService;
        private readonly IBudgetRequestService _budgetRequestService;

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
            IBudgetRequestService budgetRequestService)
        {
            _budgetRequestListServiceFactory = budgetRequestListServiceFactory;
            _agreementBaseService = agreementBaseService;
            _budgetRequestService = budgetRequestService;

            BudgetRequests = new List<BudgetRequestRowViewModel>();
            _displayBudgetRequests = new List<BudgetRequestRowViewModel>();
            FilterViewModel = new FilterViewModel();

            PageNumber = 1;
            PageSize = 10;
            Total = 0;
        }

        public event System.Action ViewModelChanged;

        public IFilterViewModel FilterViewModel { get; }

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

            BudgetRequestUpdateOutput output = _agreementBaseService.ApproveListByRole(ids, role);

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
            BudgetRequestViewModel = new BudgetRequestViewModel(id, _budgetRequestService);
        }

        private void FilterCustomization()
        {
            FilterViewModel.RequesterIds.AddRange(_displayBudgetRequests.Select(br=>br.BudgetRequest.RequesterId).ToList().Distinct());
            FilterViewModel.States.AddRange(_displayBudgetRequests.Select(br => br.BudgetRequest.State).ToList().Distinct());
            FilterViewModel.DepartmentIds.AddRange(_displayBudgetRequests.Select(br => br.BudgetRequest.DepartmentId).ToList().Distinct());
            FilterViewModel.UnitIds.AddRange(_displayBudgetRequests.Select(br => br.BudgetRequest.UnitId).ToList().Distinct());
        }
    }
}
