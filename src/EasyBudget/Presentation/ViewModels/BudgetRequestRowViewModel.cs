using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestRowViewModel : IBudgetRequestRowViewModel
    {
        public BudgetRequestMainListDto BudgetRequest { get; }
        public bool IsApproveable { get; set; }
        public bool IsSelected { get; set; }
        
        private Role role = new Role()
        {
            Id = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            Name = "Approver",
        };

        public BudgetRequestRowViewModel(BudgetRequestMainListDto budgetRequest)
        {
            BudgetRequest = budgetRequest;
            CheckApprove();
        }

        private void CheckApprove()
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        IsApproveable = true;
                    }
                    break;
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated | BudgetRequest.State == BudgetState.PostponedDirector)
                    {
                        IsApproveable = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector| BudgetRequest.State == BudgetState.PostponedFinDirector)
                    {
                        IsApproveable = true;
                    }
                    break;
                default:
                    IsApproveable = false;
                    break;
            }
        }
    }
}
