using System;
using DataAccess;
using EasyBudget.Common.DataAccess.Commands;
using EasyBudget.Common.Model;

namespace ConsoleMykyta
{
    class Program
    {
        static void Main(string[] args)
        {
            IBudgetRequestAccess access = new BudgetRequestAccess();
            Guid id = Guid.Empty;
            BudgetRequest budgetRequest = access.GetBudgetRequest(id);
        }
    }
}
