using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestEntityFactory
    {
        IBudgetRequestViewModel GetNewRequestViewModel();
        IBudgetRequestViewModel GetExistedRequestViewModel(Guid id);
    }
}
