using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetDescriptionAccess
    {
        void Add(BudgetDescription description);
        BudgetDescription Get(Guid id);
    }
}
