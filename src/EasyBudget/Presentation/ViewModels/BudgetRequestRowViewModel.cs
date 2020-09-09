using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestRowViewModel : IBudgetRequestRowViewModel
    {
        private IUserService userService;

        public BudgetRequestMainListDto BudgetRequest { get; }
        public bool IsEditable { get; set; }
        private Guid userId;

        public BudgetRequestMainListDto LoadData()
        {
            return null;
        }

    }
}
