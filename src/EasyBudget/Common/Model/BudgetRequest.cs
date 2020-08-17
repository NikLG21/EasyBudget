using System;
using System.Collections.Generic;
using Common.Model.Security;

namespace Common.Model
{
    public class BudgetRequest : Entity
    {
        public string Name { get; set; }
        public List<BudgetDescription> BudgetDescriptions { get; }
        public User Requester { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateDeadline { get; set; }
        public int EstimatedSum { get; set; }
        public int ExactSum { get; set; }
        public User Executor { get; set; }
        public DateTime DateStartExecution { get; set; }
        private DateTime DateDeadlineExecution { get; set; }
        private DateTime DateEndExecution { get; set; }
        private int ExpenceSum { get; set; }
        private ChainOfApprovals Chain { get; }
    }
}
