using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Outputs
{
    public class BudgetRequestUpdateOutput
    {
        public BudgetRequest Request { get; private set; }
        public string Message { get; private set; }

        public BudgetRequestUpdateOutput(BudgetRequest request, string message)
        {
            Request = request;
            Message = message;
        }
    }
}
