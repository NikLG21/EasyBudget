using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListViewModel : IBudgetRequestListViewModel
    {
        public void LoadData()
        {

        }


        private List<BudgetRequestMainListDto> budgetRequests = new List<BudgetRequestMainListDto>();
        public List<BudgetRequestMainListDto> BudgetRequests 
        {
            get
            {
                return budgetRequests;
            }
        }
    }
}
