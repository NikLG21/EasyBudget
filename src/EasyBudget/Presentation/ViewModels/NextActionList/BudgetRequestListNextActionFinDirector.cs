using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels.NextActionList
{
    public class BudgetRequestListNextActionFinDirector: IBudgetRequestListNextAction
    {
        private IBudgetRequestService budgetRequestService;
        private Guid userId;
        public BudgetRequestListNextActionFinDirector(IBudgetRequestService budgetRequestService)
        {
            this.budgetRequestService = budgetRequestService;
        }

        public List<BudgetRequestMainListDto> BudgetRequests { get; }

        public List<BudgetRequestMainListDto> LoadData()
        {
            BudgetRequests.AddRange(budgetRequestService.GetListUnapprovedFinDirector(userId));
            return BudgetRequests;
        }
    }
}
