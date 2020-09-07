using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListNextAction
    {
        List<BudgetRequestMainListDto> BudgetRequests { get; }
        List<BudgetRequestMainListDto> LoadData();
    }
}
