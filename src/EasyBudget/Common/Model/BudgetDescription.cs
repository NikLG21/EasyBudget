using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetDescription : Entity
    {
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
