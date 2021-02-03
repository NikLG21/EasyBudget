using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetRequestAccess
    {
        void Add(BudgetRequest request);
        void Update(BudgetRequest request);
        //TODO: Move to command. Done
        void Delete(Guid id);
        BudgetRequest Get(Guid id);
        //TODO: What is it? GetSimpleRequest: Request without Include. Get: Request with Include
        BudgetRequest GetSimpleRequest(Guid id);
    }
}
