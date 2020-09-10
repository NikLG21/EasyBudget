using System;
using System.Collections.Generic;
using EasyBudget.Common.Business;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel: IBudgetRequestListViewModel
    {
        private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("3148ce2c-540e-4cc4-a372-42e0c29a478b"),
            CurrentRoleId = Guid.Parse("cb294f90-3cb6-4169-9916-1e39d7f4bd3d"),
            CurrentRoleName = "Ініціатор запиту",
        };

        private Role role = new Role()
        {
            Actions = null,
            Department = null,
            Id = Guid.Parse("cb294f90-3cb6-4169-9916-1e39d7f4bd3d"),
            Name = "Ініціатор запиту",
            Users = new List<User>()
        };

        private IBudgetRequestListServiceFactory _budgetRequestListServiceFactory;
        public List<BudgetRequestMainListDto> BudgetRequests { get; }

        public BudgetRequestListViewModel(IBudgetRequestListServiceFactory budgetRequestListServiceFactory)
        {
            _budgetRequestListServiceFactory = budgetRequestListServiceFactory;
        }

        public void LoadData()
        {
            IBudgetRequestListService budgetRequestListService = _budgetRequestListServiceFactory.Create(role);
            BudgetRequests.Clear();
            BudgetRequests.AddRange(budgetRequestListService.GetList(userInfo));
        }

        public List<BudgetRequestMainListDto> LoadNextActionList()
        {
            List<BudgetRequestMainListDto> list = new List<BudgetRequestMainListDto>();
            switch (role.Name)
            {
                case "Ініціатор запиту":
                    foreach (BudgetRequestMainListDto request in BudgetRequests)
                    {
                        if (request.State == BudgetState.Requested)
                        {
                            list.Add(request);
                        }
                    }
                    break;
                case "Затверджувач":
                    foreach (BudgetRequestMainListDto request in BudgetRequests)
                    {
                        if (request.State == BudgetState.Requested)
                        {
                            list.Add(request);
                        }
                    }
                    break;
                case "Виконавець":
                    foreach (BudgetRequestMainListDto request in BudgetRequests)
                    {
                        if (request.State == BudgetState.ApprovedFirstLine|request.State == BudgetState.Execution)
                        {
                            list.Add(request);
                        }
                    }
                    break;
                case "Виконавець IT":
                    foreach (BudgetRequestMainListDto request in BudgetRequests)
                    {
                        if (request.State == BudgetState.Requested | request.State == BudgetState.Execution)
                        {
                            list.Add(request);
                        }
                    }
                    break;
                case "Директор":
                    foreach (BudgetRequestMainListDto request in BudgetRequests)
                    {
                        if (request.State == BudgetState.ExecutorEstimated)
                        {
                            list.Add(request);
                        }
                    }
                    break;
                case "Фінансовий директор":
                    foreach (BudgetRequestMainListDto request in BudgetRequests)
                    {
                        if (request.State == BudgetState.ApprovedDirector)
                        {
                            list.Add(request);
                        }
                    }
                    break;
                default:
                    break;
            }

            return list;
        }
    }
}
