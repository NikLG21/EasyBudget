using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Commands
{
    public interface IBudgetRequestAccess
    {
        void AddBudgetRequest(BudgetRequest request);
        void UpdateBudgetRequest(BudgetRequest request);
        void DeleteBudgetRequest(Guid budgetRequestId);
        BudgetRequest GetBudgetRequest(Guid budgetRequestId);

        List<BudgetRequest> GetBudgetRequestByOriginatorList(Guid userId);
        List<BudgetRequest> GetBudgetRequestByApproverList(Guid userId);
    }
}
