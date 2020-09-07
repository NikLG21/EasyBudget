using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels.NextActionList
{
    public class BudgetRequestListNextActionExecutor : IBudgetRequestListNextAction
    {
        private IBudgetRequestService budgetRequestService;
        private Guid userId;

        public BudgetRequestListNextActionExecutor(IBudgetRequestService budgetRequestService, IUserService userService)
        {
            this.budgetRequestService = budgetRequestService;
            this.userService = userService;
        }

        public List<BudgetRequestMainListDto> BudgetRequests { get; }

        public List<BudgetRequestMainListDto> LoadData()
        {
            BudgetRequests.AddRange(budgetRequestService.GetListUncheckedExecutor(userId));
            return BudgetRequests;
        }
    }
}
