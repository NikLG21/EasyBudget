using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Interfaces;
using EasyBudget.Presentation.Utils;

namespace EasyBudget.Presentation.ViewModels
{
    public class FilterViewModel : IFilterViewModel
    {
        public bool OnGoingFilterIsActive { get; set; }
        public bool SelectedFilterIsActive { get; set; }
        public List<PairGuid> Requesters { get; private set; }
        public List<PairGuid> Departments { get; private set; }
        public List<PairGuid> Units { get; private set; }
        public List<PairEnum<BudgetState>> States { get; private set; }

        public Guid Requester { get; set; }
        public Guid Department { get; set; }
        public Guid Unit { get; set; }
        public BudgetState State { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public FilterViewModel()
        {
            Requesters = new List<PairGuid>();
            Departments = new List<PairGuid>();
            Units = new List<PairGuid>();
            States = new List<PairEnum<BudgetState>>();
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
