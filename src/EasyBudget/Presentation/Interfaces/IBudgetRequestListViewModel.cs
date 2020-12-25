using System;
using System.Collections.Generic;
using EasyBudget.Presentation.Enums;
using EasyBudget.Presentation.Utils;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel
    {
        event Action ViewModelChanged;

        IBudgetRequestViewModel BudgetRequestViewModel { get; }
        IFilterViewModel FilterViewModel { get; }
        SortingList Sorting { get; set; }
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
        void ReturnToList();
        void ChangeSorting(SortingEntity entity, bool direction);
        void ApproveCertainRequest(BudgetRequestRowViewModel request);
    }
}
