using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestViewModel
    {
        BudgetRequest BudgetRequest { get; set; }
        //TODO: I think this is not enough. We need flags for editing different fields
        bool IsEditable { get; set; }
        bool InEditMode { get; set; }
    }
}
