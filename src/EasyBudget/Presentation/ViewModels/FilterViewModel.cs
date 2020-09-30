using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class FilterViewModel : IFilterViewModel
    {
        public List<string> RequesterNames { get; }
        public List<string> DepartmentNames { get; }
        public List<string> UnitNames { get; }
        public List<BudgetState> States { get; }

        public FilterViewModel()
        {
            RequesterNames = new List<string>();
            DepartmentNames = new List<string>();
            UnitNames = new List<string>();
            States = new List<BudgetState>();
        }

    }
}
