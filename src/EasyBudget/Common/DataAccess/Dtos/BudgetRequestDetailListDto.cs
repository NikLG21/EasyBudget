using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class BudgetRequestDetailListDto : Entity
    {
        public string Name { get; set; }
        public string RequesterName { get; set; }
        public string ApproverName { get; set; }
        public string ExecutorName { get; set; }
        public string DepartmentName { get; set; }
        public string UnitName { get; set; }
        public DateTime DateRequested { get; set; }
        public BudgetState State { get; set; }
        public decimal? RealPrice { get; set; }
    }
}
