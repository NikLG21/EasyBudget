using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetDescription : Entity
    {
        public BudgetDescription()
        {
        }

        public BudgetDescription(Guid id) : base(id)
        {
        }

        public string Description { get; set; }

        public User User { get; set; }
        public Guid? UserId { get; set; }

        public BudgetRequest BudgetRequest { get; set; }
        public Guid? BudgetRequestId { get; set; }

        public DateTime Date { get; set; }
    }
}
