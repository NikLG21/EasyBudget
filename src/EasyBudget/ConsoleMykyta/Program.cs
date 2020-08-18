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
            BudgetRequest request = new BudgetRequest();
            access.AddBudgetRequest(request);
            Guid id = request.Id;
            BudgetRequest budgetRequest = access.GetBudgetRequest(id);
            Console.WriteLine(budgetRequest.Id);
        }
    }
}
