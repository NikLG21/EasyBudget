using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels.NextActionList
{
    public class BudgetRequestListNextActionDirector : IBudgetRequestListNextAction
    {
        private IBudgetRequestService budgetRequestService;
        private Guid userId;

        public BudgetRequestListNextActionDirector(IBudgetRequestService budgetRequestService)
        {
            this.budgetRequestService = budgetRequestService;
        }

        public List<BudgetRequestMainListDto> BudgetRequests { get; }

        public List<BudgetRequestMainListDto> LoadData()
        {
            BudgetRequests.AddRange(budgetRequestService.GetListUnapprovedDirector(userId));
            return BudgetRequests;
        }
    }
}
