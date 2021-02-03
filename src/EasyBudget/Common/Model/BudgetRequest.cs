using System;
using System.Collections.Generic;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetRequest : Entity
    {
        public BudgetRequest()
        {
            BudgetDescriptions = new List<BudgetDescription>();
            BudgetHistories = new List<BudgetHistory>();
        }

        public string Name { get; set; }

        public User Requester { get; set; }
        public Guid RequesterId { get; set; }

        public User Approver { get; set; }
        public Guid? ApproverId { get; set; }

        public User Executor { get; set; }
        public Guid? ExecutorId { get; set; }

        public Department Department { get; set; }
        public Guid DepartmentId { get; set; }

        public Unit Unit { get; set; }
        public Guid UnitId { get; set; }

        public DateTime DateRequested { get; set; }
        public DateTime? DateRequestedDeadline { get; set; }
        public DateTime? DateDirectorApprove { get; set; }
        public DateTime? DateStartExecution { get; set; }
        public DateTime? DateDeadlineExecution { get; set; }
        public DateTime? DateEndExecution { get; set; }

        public decimal? EstimatedPrice { get; set; }
        public decimal? RealPrice { get; set; }

        public BudgetState State { get; set; }

        public List<BudgetDescription> BudgetDescriptions { get; }
        public List<BudgetHistory> BudgetHistories { get; }
    }
}
