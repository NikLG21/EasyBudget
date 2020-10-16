using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class FilterViewModel : IFilterViewModel
    {
        public bool OnGoingFilterIsActive { get; set; }
        public bool SelectedFilterIsActive { get; set; }

        public List<Guid> RequesterIds { get; private set; }
        public List<Guid> DepartmentIds { get; private set; }
        public List<Guid> UnitIds { get; private set; }
        public List<BudgetState> States { get; private set; }

        public Guid Requester { get; set; }
        public Guid Department { get; set; }
        public Guid Unit { get; set; }
        public BudgetState State { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public FilterViewModel()
        {
            RequesterIds = new List<Guid>();
            DepartmentIds = new List<Guid>();
            UnitIds = new List<Guid>();
            States = new List<BudgetState>();
            Requester = Guid.Empty;
            Department = Guid.Empty;
            Unit = Guid.Empty;
            State = BudgetState.Undefined;
            To = DateTime.Today;
            OnGoingFilterIsActive = false;
            SelectedFilterIsActive = false;
        }

    }
}
