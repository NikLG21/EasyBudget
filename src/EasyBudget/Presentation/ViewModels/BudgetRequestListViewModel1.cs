using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel1 : IBudgetRequestListViewModel1
    {
        private Guid userId;
        private Role role;
        private IBudgetRequestService budgetRequestService;
        private IUserService userService;
        public List<BudgetRequestMainListDto> BudgetRequest { get; }
        public List<BudgetRequestMainListDto> LoadAllList()
        {
            switch (role.Name)
            {
                case "Ініціатор запиту":
                    BudgetRequest.AddRange(budgetRequestService.GetListByRequestor(userId, DateTime.MinValue, DateTime.MaxValue));
                    break;
                case "Затверджувач":
                    BudgetRequest.AddRange(budgetRequestService.GetListByApprover(userId, DateTime.MinValue, DateTime.MaxValue));
                    break;
                case "Виконавець":
                    BudgetRequest.AddRange(budgetRequestService.GetListByExecutor(userId, DateTime.MinValue, DateTime.MaxValue));
                    break;
                case "Виконавець IT":
                    BudgetRequest.AddRange(budgetRequestService.GetListByApprover(userId, DateTime.MinValue, DateTime.MaxValue));
                    break;
                case "Директор":
                    BudgetRequest.AddRange(budgetRequestService.GetListByTime(userId, DateTime.MinValue, DateTime.MaxValue));
                    break;
                case "Фінансовий директор":
                    BudgetRequest.AddRange(budgetRequestService.GetListByTime(userId, DateTime.MinValue, DateTime.MaxValue));
                    break;
                default:
                    break;
            }

            return BudgetRequest;
        }
        public List<BudgetRequestMainListDto> LoadNextActionList()
        {
            switch (role.Name)
            {
                case "Ініціатор запиту":
                    BudgetRequest.AddRange(budgetRequestService.GetListUnapprovedRequestor(userId));
                    break;
                case "Затверджувач":
                    BudgetRequest.AddRange(budgetRequestService.GetListUnapprovedApprover(userId, userService.GetUser(userId, userId).Unit));
                    break;
                case "Виконавець":
                    BudgetRequest.AddRange(budgetRequestService.GetListUncheckedExecutor(userId,role.Department));
                    BudgetRequest.AddRange(budgetRequestService.GetListExecutionExecutor(userId, role.Department));
                    break;
                case "Виконавець IT":
                    BudgetRequest.AddRange(budgetRequestService.GetListUncheckedExecutor(userId, role.Department));
                    BudgetRequest.AddRange(budgetRequestService.GetListExecutionExecutor(userId, role.Department));
                    break;
                case "Директор":
                    BudgetRequest.AddRange(budgetRequestService.GetListUnapprovedDirector(userId));
                    BudgetRequest.AddRange(budgetRequestService.GetListPostponedDirector(userId));
                    break;
                case "Фінансовий директор":
                    BudgetRequest.AddRange(budgetRequestService.GetListUnapprovedFinDirector(userId));
                    BudgetRequest.AddRange(budgetRequestService.GetListPostponedFinDirector(userId));
                    break;
                default:
                    break;
            }

            return BudgetRequest;
        }
    }
}
