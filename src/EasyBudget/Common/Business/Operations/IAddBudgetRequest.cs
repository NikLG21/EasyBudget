using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Operations
{
    interface IAddBudgetRequest
    {
        void AddBudgetRequest(BudgetRequest request, User user);
    }
}
