using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel
    {
        event System.Action ViewModelChanged;

        IFilterViewModel FilterViewModel { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int Total { get; set; }
        List<BudgetRequestRowViewModel> BudgetRequests { get; }
        List<BudgetRequestRowViewModel> BudgetRequestPage { get; }
        void LoadData();
        void ApproveRequests();
        void ApplySelection();
        public void OnGoingList();
    }
}
