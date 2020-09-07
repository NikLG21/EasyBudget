using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestListViewModel1
    {
        List<BudgetRequestMainListDto> BudgetRequest { get; }
        List<BudgetRequestMainListDto> LoadAllList();
        List<BudgetRequestMainListDto> LoadNextActionList();
    }
}
