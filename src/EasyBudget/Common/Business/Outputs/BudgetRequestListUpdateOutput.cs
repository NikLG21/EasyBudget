using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Outputs
{
    public class BudgetRequestListUpdateOutput
    {
        public List<BudgetRequestMainListDto> SuccessUpdatedBudgetRequests { get; set; }
        public List<BudgetRequestMainListDto> FailedUpdatedBudgetRequests { get; set; }
        public List<string> Messages { get; set; }

        public BudgetRequestListUpdateOutput()
        {
            SuccessUpdatedBudgetRequests = new List<BudgetRequestMainListDto>();
            FailedUpdatedBudgetRequests = new List<BudgetRequestMainListDto>();
            Messages = new List<string>();
        }
    }
}
