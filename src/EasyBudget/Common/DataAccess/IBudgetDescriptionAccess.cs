using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetDescriptionAccess
    {
        void Add(BudgetDescription description);
        BudgetDescription Get(Guid id);
    }
}
