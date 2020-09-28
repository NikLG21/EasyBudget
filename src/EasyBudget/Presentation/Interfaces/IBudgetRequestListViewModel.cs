using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int Total { get; set; }
        List<BudgetRequestRowViewModel> BudgetRequests { get; }
        void LoadData();
        void ApproveRequests();
    }
}
