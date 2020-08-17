﻿using System;
using System.Collections.Generic;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetRequest : Entity
    {
        public string Name { get; set; }

        public User Requester { get; set; }
        public User Approver { get; set; }
        public User Executor { get; set; }

        public Department Department { get; set; }

        public DateTime DateRequested { get; set; }
        public DateTime DateRequestedDeadline { get; set; }
        public DateTime DateDirectorApprove { get; set; }
        public DateTime DateStartExecution { get; set; }
        public DateTime DateDeadlineExecution { get; set; }
        public DateTime DateEndExecution { get; set; }

        public decimal EstimatedSum { get; set; }
        public decimal ExactSum { get; set; }
        public decimal ExpenceSum { get; set; }

        public ChainOfApprovals Chain { get; }

        public List<BudgetDescription> BudgetDescriptions { get; } = new List<BudgetDescription>();
    }
}
