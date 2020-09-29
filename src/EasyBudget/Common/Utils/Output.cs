using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Utils
{
    //TODO: Bad name. Please rename to BudgetRequestUpdateOutput. Also please move to the Business/Outputs
    public class Output
    {
        //TODO: Bad name. Please rename to SuccessUpdatedBudgetRequests
        public List<BudgetRequestMainListDto> Success { get; set; }
        //TODO: Bad name. Please rename to FailedUpdatedBudgetRequests
        public List<BudgetRequestMainListDto> Fail { get; set; }
        public List<string> Messages { get; set; }

        public Output()
        {
            Success = new List<BudgetRequestMainListDto>();
            Fail = new List<BudgetRequestMainListDto>();
            Messages = new List<string>();
        }
    }
}
