using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class BudgetRequestMainListDto : Entity
    {
        public string Name { get; set; }

        public string RequesterName { get; set; }
        public string DepartmentName { get; set; }

        public DateTime DateRequested { get; set; }
        public BudgetState State { get; set; }

        public decimal? RealPrice { get; set; }
    }
}
