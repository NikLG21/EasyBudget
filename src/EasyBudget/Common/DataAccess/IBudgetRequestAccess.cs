using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetRequestAccess
    {
        void Add(BudgetRequest request);
        void Update(BudgetRequest request);
        void Delete(Guid id);
        BudgetRequest Get(Guid id);
        BudgetRequest GetSimple(Guid id);
    }
}
