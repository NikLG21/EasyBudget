using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestRowViewModel
    {
        BudgetRequestMainListDto BudgetRequest { get; }
        bool IsApproveable { get; set; }
        bool IsSelected { get; set; }
    }
}
