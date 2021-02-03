using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetHistory : Entity
    {
        public BudgetHistory()
        {
        }

        public BudgetHistory(Guid id) : base(id)
        {
        }

        public BudgetRequest BudgetRequest { get; set; }
        public Guid? BudgetRequestId { get; set; }

        public User User { get; set; }
        public Guid? UserId { get; set; }

        public DateTime Date { get; set; }

        public string ActionName { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
    }
}
