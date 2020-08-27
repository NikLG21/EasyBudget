using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetHistory :Entity
    {
        public BudgetRequest BudgetRequest { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string ActionMade { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
    }
}
