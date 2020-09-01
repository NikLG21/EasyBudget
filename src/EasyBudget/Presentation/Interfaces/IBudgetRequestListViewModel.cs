using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel
    {
        List<BudgetRequestMainListDto> BudgetRequests { get; }
        void LoadData();
    }
}
