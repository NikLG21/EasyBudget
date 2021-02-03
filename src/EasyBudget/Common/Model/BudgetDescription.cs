using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetDescription : Entity
    {
        public string Description { get; set; }

        //TODO: Add UsertId. Done
        public User User { get; set; }

        //TODO: Add BudgetRequestId. Done
        public BudgetRequest BudgetRequest { get; set; }

        public Guid UserId { get; set; }

        public Guid BudgetRequestId { get; set; }

        public DateTime Date { get; set; }
    }
}
