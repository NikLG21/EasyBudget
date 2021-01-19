using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Enums;
using EasyBudget.Presentation.Utils;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestViewModel
    {
        event Action EntityViewModelChanged;
        BudgetRequest BudgetRequest { get; set; }
        BudgetRequest ChangedBudgetRequest { get; set; }
        
        List<PairGuid> Departments { get; }

        FieldsStates NameField { get; set; }
        FieldsStates DateRequestedDeadlineField { get; set; }
        FieldsStates DateDeadlineExecutionField { get; set; }
        FieldsStates EstimatedPriceField { get; set; }
        FieldsStates RealPriceField { get; set; }
        bool IsEditable { get; set; }
        bool ApproveAble { get; set; }
        bool RejectAble { get; set; }
        bool PostponeAble { get; set; }
        bool InEditMode { get; set; }
        bool NewRequestMode { get; set; }
        void ChangeEditMode();
        void ApproveRequest();
        void CreateNewRequest();
        void CancelChanges();
        void RejectRequest();
        void PostponeRequest();

    }
}
