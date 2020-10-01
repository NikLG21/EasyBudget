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
        private int _pageNumber;

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
        public List<BudgetRequestRowViewModel> BudgetRequestPage
        {
            get
            {
                if (Total - PageSize * (PageNumber-1) > 0)
                {
                    return BudgetRequests.Skip(PageSize*(PageNumber-1)).Take(PageSize).ToList();
                }
                else
                {
                    return BudgetRequests.Skip(PageSize * (PageNumber - 1)).Take(Total - PageSize * PageNumber).ToList();
                }

                return null;
            }
        }

        public BudgetRequestListViewModel(IBudgetRequestListServiceFactory budgetRequestListServiceFactory, IAgreementBaseService agreementBaseService)
        {
            BudgetRequests = new List<BudgetRequestRowViewModel>();
            
            _budgetRequestListServiceFactory = budgetRequestListServiceFactory;
            _agreementBaseService = agreementBaseService;
            PageNumber = 1;
            PageSize = 3;
        }

        public void LoadData()
        {
            IBudgetRequestListService budgetRequestListService = _budgetRequestListServiceFactory.Create(role);
            foreach (BudgetRequestMainListDto request in budgetRequestListService.GetList(userInfo))
            {
                BudgetRequests.Add(new BudgetRequestRowViewModel(request));
            }

            Total = BudgetRequests.Count;
        }

        public void ApproveRequests()
        {
            List<Guid> ids = new List<Guid>();
            if (Total - PageSize * PageNumber > 0)
            {
                for (int i = PageSize * (PageNumber - 1); i < PageSize * PageNumber; i++)
                {
                    if (BudgetRequests[i].IsApproveable && BudgetRequests[i].IsSelected)
                    {
                        ids.Add(BudgetRequests[i].BudgetRequest.Id);
                    }
                }
            }
            else
            {
                for (int i = PageSize * (PageNumber - 1); i < Total; i++)
                {
                    if (BudgetRequests[i].IsApproveable && BudgetRequests[i].IsSelected)
                    {
                        ids.Add(BudgetRequests[i].BudgetRequest.Id);
                    }
                }

            }

            if (ids.Count == 0)
            {
                return;
            }
            BudgetRequestUpdateOutput output = _agreementBaseService.ApproveListByRole(ids, role);

            foreach (BudgetRequestMainListDto request in output.SuccessUpdatedBudgetRequests)
            {
                if (Total - PageSize * PageNumber > 0)
                {
                    for (int i = PageSize * (PageNumber - 1); i < PageSize * PageNumber; i++)
                    {
                        if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                        {
                            BudgetRequests[i] = new BudgetRequestRowViewModel(request);
                        }
                    }
                }
                else
                {
                    for (int i = PageSize * (PageNumber - 1); i < Total; i++)
                    {
                        if (BudgetRequests[i].IsApproveable && BudgetRequests[i].IsSelected)
                        {
                            if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                            {
                                BudgetRequests[i] = new BudgetRequestRowViewModel(request);
                            }
                        }
                    }
                }
            }

            foreach (BudgetRequestMainListDto request in output.FailedUpdatedBudgetRequests)
            {
                if (Total - PageSize * PageNumber > 0)
                {
                    for (int i = PageSize * (PageNumber - 1); i < PageSize * PageNumber; i++)
                    {
                        if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id))
                        {
                            BudgetRequests[i] = new BudgetRequestRowViewModel(request);

                        }
                    }
                }
                else
                {
                    for (int i = PageSize * (PageNumber - 1); i < Total; i++)
                    {
                        if (request.Id.Equals(BudgetRequests[i].BudgetRequest.Id)) 
                        { 
                            BudgetRequests[i] = new BudgetRequestRowViewModel(request);

                        }
                    }
                }
            }

        }

    }
}
