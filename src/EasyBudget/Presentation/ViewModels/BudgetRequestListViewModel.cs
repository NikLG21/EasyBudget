using System;
using System.Collections.Generic;
using EasyBudget.Common.Business;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Common.Utils;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel: IBudgetRequestListViewModel
    {
        private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("3148ce2c-540e-4cc4-a372-42e0c29a478b"),
            CurrentRoleId = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            CurrentRoleName = "Director",
        };
        
        private Role role = new Role()
        {
            Actions = null,
            Department = null,
            Id = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            Name = "Director",
            Users = new List<User>()
        };

        private IBudgetRequestListServiceFactory _budgetRequestListServiceFactory;
        private IAgreementBudgetRequestService _agreementBudgetRequestService;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<BudgetRequestRowViewModel> BudgetRequests { get; } = new List<BudgetRequestRowViewModel>();

        public BudgetRequestListViewModel(IBudgetRequestListServiceFactory budgetRequestListServiceFactory, IAgreementBudgetRequestService agreementBudgetRequestService)
        {
            _budgetRequestListServiceFactory = budgetRequestListServiceFactory;
            _agreementBudgetRequestService = agreementBudgetRequestService;
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
            Output output;
            switch (role.Name)
            {
                case RoleNames.Approver:
                    output = _agreementBudgetRequestService.ApproveListFirstLine(ids);
                    break;
                case RoleNames.Director:
                    output = _agreementBudgetRequestService.ApproveListDirector(ids);
                    break;
                case RoleNames.FinDirector:
                    output = _agreementBudgetRequestService.ExecutionStartedListFinDirector(ids);
                    break;
                default:
                    output = null;
                    break;
            }

            foreach (BudgetRequestMainListDto request in output.Success)
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

            foreach (BudgetRequestMainListDto request in output.Fail)
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

        }
            //public List<BudgetRequestMainListDto> LoadNextActionList()
            //{
            //    List<BudgetRequestMainListDto> list = new List<BudgetRequestMainListDto>();
            //    switch (role.Name)
            //    {
            //        case "Ініціатор запиту":
            //            foreach (BudgetRequestMainListDto request in BudgetRequests)
            //            {
            //                if (request.State == BudgetState.Requested)
            //                {
            //                    list.Add(request);
            //                }
            //            }
            //            break;
            //        case "Затверджувач":
            //            foreach (BudgetRequestMainListDto request in BudgetRequests)
            //            {
            //                if (request.State == BudgetState.Requested)
            //                {
            //                    list.Add(request);
            //                }
            //            }
            //            break;
            //        case "Виконавець":
            //            foreach (BudgetRequestMainListDto request in BudgetRequests)
            //            {
            //                if (request.State == BudgetState.ApprovedFirstLine|request.State == BudgetState.Execution)
            //                {
            //                    list.Add(request);
            //                }
            //            }
            //            break;
            //        case "Виконавець IT":
            //            foreach (BudgetRequestMainListDto request in BudgetRequests)
            //            {
            //                if (request.State == BudgetState.Requested | request.State == BudgetState.Execution)
            //                {
            //                    list.Add(request);
            //                }
            //            }
            //            break;
            //        case "Директор":
            //            foreach (BudgetRequestMainListDto request in BudgetRequests)
            //            {
            //                if (request.State == BudgetState.ExecutorEstimated)
            //                {
            //                    list.Add(request);
            //                }
            //            }
            //            break;
            //        case "Фінансовий директор":
            //            foreach (BudgetRequestMainListDto request in BudgetRequests)
            //            {
            //                if (request.State == BudgetState.ApprovedDirector)
            //                {
            //                    list.Add(request);
            //                }
            //            }
            //            break;
            //        default:
            //            break;
            //    }

            //    return list;
            //}
        }
}
