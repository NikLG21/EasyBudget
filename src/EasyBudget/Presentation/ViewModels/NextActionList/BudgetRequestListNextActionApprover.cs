using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels.NextActionList
{
    public class BudgetRequestListNextActionApprover : IBudgetRequestListNextAction
    {
        private IBudgetRequestService budgetRequestService;
        private IUserService userService;
        private Guid userId;

        public BudgetRequestListNextActionApprover(IBudgetRequestService budgetRequestService, IUserService userService)
        {
            this.budgetRequestService = budgetRequestService;
            this.userService = userService;
        }

        public List<BudgetRequestMainListDto> BudgetRequests { get; }
        
        public List<BudgetRequestMainListDto> LoadData()
        {
            User user = userService.GetUser(userId, userId);
            BudgetRequests.AddRange(budgetRequestService.GetListUnapprovedApprover(userId, user.Unit)); 
            return BudgetRequests;
        }
    }
}
