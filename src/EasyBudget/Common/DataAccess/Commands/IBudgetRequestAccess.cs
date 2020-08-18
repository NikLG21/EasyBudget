using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess.Commands
{
    public interface IBudgetRequestAccess
    {
        void AddBudgetRequestInBase(BudgetRequest request);
        void DeleteBudgetRequestFromBase(Guid id);
        void ChangeBudgetRequestInBase(BudgetRequest request, Guid id);
        void ShowBudgetRequestFromBase(Guid id);
        void ShowBudgetRequestListFromBase(User user, BudgetRequestFilter filter);
    }
}
