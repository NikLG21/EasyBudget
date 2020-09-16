using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel
    {
        List<BudgetRequestRowViewModel> BudgetRequests { get; }
        void LoadData();
    }
}
