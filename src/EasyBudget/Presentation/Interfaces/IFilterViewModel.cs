using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Presentation.Interfaces
{
     public interface IFilterViewModel
    {
        bool OnGoingFilterIsActive { get; set; }
        bool SelectedFilterIsActive { get; set; }
        List<Guid> RequesterIds { get; }
        List<Guid> DepartmentIds { get; }
        List<Guid> UnitIds { get; }
        List<BudgetState> States { get; }
        Guid Requester { get; set; }
        Guid Department { get; set; }
        Guid Unit { get; set; }
        BudgetState State { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
