using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestViewModel : IBudgetRequestViewModel
    {
        public BudgetRequest BudgetRequest { get; }
        public void LoadData()
        {
            
        }
    }
}
