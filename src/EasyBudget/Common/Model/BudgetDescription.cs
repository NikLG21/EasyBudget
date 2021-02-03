using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetDescription : Entity
    {
        public string Description { get; set; }

        //TODO: Add UsertId
        public User User { get; set; }

        //TODO: Add BudgetRequestId
        public BudgetRequest BudgetRequest { get; set; }

        public DateTime Date { get; set; }
    }
}
