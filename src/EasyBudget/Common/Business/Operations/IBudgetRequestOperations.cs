using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Operations
{
    public interface IBudgetRequestOperations
    {
        void AddBudgetRequest(BudgetRequest request, User user);
        void EditBudgetRequest(BudgetRequest request);
        void DeleteBudgetRequest(Guid id);
        BudgetRequest ViewingBudgetRequest(Guid id);
        List<BudgetRequest> ViewingBudgetRequestsList(User user);
    }
}
