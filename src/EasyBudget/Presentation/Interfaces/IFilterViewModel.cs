using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Utils;

namespace EasyBudget.Presentation.Interfaces
{
     public interface IFilterViewModel
    {
        bool OnGoingFilterIsActive { get; set; }
        bool SelectedFilterIsActive { get; set; }

        //TODO: I think we have to make lists with Pair
        //List<PairGuid> Requesters { get; }
        //List<PairGuid> Departments { get; }
        //List<PairGuid> Units { get; }
        //List<PairEnum<BudgetState>> States { get; }

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
