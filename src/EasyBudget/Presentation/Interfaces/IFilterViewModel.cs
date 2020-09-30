using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Presentation.Interfaces
{
     public interface IFilterViewModel
    {
        List<string> RequesterNames { get; }
        List<string> DepartmentNames { get; }
        List<string> UnitNames { get; }
        List<BudgetState> States { get; }
    }
}
