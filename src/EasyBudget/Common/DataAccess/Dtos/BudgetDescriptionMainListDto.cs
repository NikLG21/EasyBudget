using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class BudgetDescriptionMainListDto : Entity
    {
        public Guid BudgetRequestId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}
