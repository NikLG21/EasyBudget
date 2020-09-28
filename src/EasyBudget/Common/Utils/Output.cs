using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Utils
{
    public class Output
    {
        public List<BudgetRequestMainListDto> Success { get; set; }
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
