using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class BudgetRequestMainListDto : Entity
    {
        public string Name { get; set; }

        public string RequesterName { get; set; }
        public Guid RequesterId { get; set; }

        public string DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }

        public string UnitName { get; set; }
        public Guid UnitId { get; set; }

        public DateTime DateRequested { get; set; }
        public BudgetState State { get; set; }
        public decimal? RealPrice { get; set; }
    }
}
