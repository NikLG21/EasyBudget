using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetRequestAccess
    {
        void Add(BudgetRequest request);
        void Update(BudgetRequest request);
        //TODO: Move to command
        void UpdateList(List<Guid> ids, BudgetState newState, Guid userId);
        void Delete(Guid id);
        BudgetRequest Get(Guid id);
        //TODO: What is it?
        BudgetRequest GetSimple(Guid id);
    }
}
