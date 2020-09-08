using System.Collections.Generic;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestListDirectorViewModel: IBudgetRequestListViewModel
    {
        private UserMainInfoDto userInfo;
        private IBudgetRequestListService budgetRequestListService;
        public List<BudgetRequestMainListDto> BudgetRequest { get; }
        public List<BudgetRequestMainListDto> LoadAllList()
        {
            return budgetRequestListService.GetList(userInfo);
        }

        public List<BudgetRequestMainListDto> LoadNextActionList()
        {
            
        }
    }
}
