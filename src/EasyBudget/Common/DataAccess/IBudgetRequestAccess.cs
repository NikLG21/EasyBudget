using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetRequestAccess
    {
        void Add(BudgetRequest request);
        void Update(BudgetRequest request);
        void UpdateList(List<Guid> ids, BudgetState newState, Guid userId);
        void Delete(Guid id);
        BudgetRequest Get(Guid id);
        BudgetRequest GetSimple(Guid id);
    }
}
