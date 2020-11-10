using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessCore
{
    public interface IBudgetRequestDbContextCoreFactory
    {
        BudgetRequestDbContextCore Create();
    }
}
