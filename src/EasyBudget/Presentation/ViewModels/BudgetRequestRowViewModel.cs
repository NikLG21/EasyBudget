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

        private Role currentRole = new Role()
        {
            Department = null,
            Id = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            Name = "Director"
        };

        public BudgetRequestRowViewModel(BudgetRequestMainListDto budgetRequest)
        {
            BudgetRequest = budgetRequest;
            CheckApprove();
        }

        private void CheckApprove()
        {
            switch (currentRole.Name)
            {
                case RoleNames.Approver:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        IsApproveable = true;
                    }
                    break;
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated | BudgetRequest.State == BudgetState.PostpondDirector)
                    {
                        IsApproveable = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector| BudgetRequest.State == BudgetState.PostpondFinDirector)
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
