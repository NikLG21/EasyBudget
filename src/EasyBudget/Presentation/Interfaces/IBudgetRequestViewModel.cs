using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Enums;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestViewModel
    {
        BudgetRequest BudgetRequest { get; set; }
        BudgetRequest ChangedBudgetRequest { get; set; }
        //TODO: I think this is not enough. We need flags for editing different fields
        
        FieldsStates NameField { get; set; }
        FieldsStates DateRequestedDeadlineField { get; set; }
        FieldsStates DateDeadlineExecutionField { get; set; }
        FieldsStates EstimatedPriceField { get; set; }
        FieldsStates RealPriceField { get; set; }
        bool IsEditable { get; set; }
        bool ApproveAble { get; set; }
        bool InEditMode { get; set; }
    }
}
