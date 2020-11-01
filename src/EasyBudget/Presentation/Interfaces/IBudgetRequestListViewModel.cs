using System;
using System.Collections.Generic;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel
    {
        event Action ViewModelChanged;

        IBudgetRequestViewModel BudgetRequestViewModel { get; }
        IFilterViewModel FilterViewModel { get; }

        int PageNumber { get; set; }
        int PageSize { get; set; }
        int Total { get; }

        List<BudgetRequestRowViewModel> BudgetRequests { get; }
        List<BudgetRequestRowViewModel> PageBudgetRequests { get; }

        void LoadData();
        void ApproveRequests();
        void ApplySelection();
        void OnGoingList();
        void OpenBudgetRequest(Guid id);
        void NewBudgetRequest();
    }
}
