using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Presentation.Interfaces
{
     public interface IFilterViewModel
    {
        bool IsActive { get; set; }
        List<Guid> RequesterIds { get; }
        List<Guid> DepartmentIds { get; }
        List<Guid> UnitIds { get; }
        List<BudgetState> States { get; }
        public Guid Requester { get; set; }
        public Guid Department { get; set; }
        public Guid Unit { get; set; }
        public BudgetState State { get; set; }
    }
}
