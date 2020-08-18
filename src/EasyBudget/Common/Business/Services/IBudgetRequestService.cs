using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestService
    {
        void AddBudgetRequest(BudgetRequest request, User user);
        void UpdateBudgetRequest(BudgetRequest request, User user);
        void DeleteBudgetRequest(BudgetRequest request, User user);

        BudgetRequest ViewBudgetRequest(Guid id, User user);

        List<BudgetRequest> ViewBudgetRequestsList(User user, DateTime start, DateTime finish);
    }
}
