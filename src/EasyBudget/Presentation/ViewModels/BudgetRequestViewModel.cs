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
    public class BudgetRequestViewModel : IBudgetRequestViewModel
    {
        private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("3148ce2c-540e-4cc4-a372-42e0c29a478b"),
            CurrentRoleId = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            CurrentRoleName = "Director",
        };

        private Role role = new Role()
        {
            Department = null,
            Id = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
            Name = "Director",
        };
        public BudgetRequest BudgetRequest { get; set; }
        public bool IsEditable { get; set; }
        public bool InEditMode { get; set; }
        private IBudgetRequestService _budgetRequestService;
        public BudgetRequestViewModel(Guid id, IBudgetRequestService budgetRequestService)
        {
            _budgetRequestService = budgetRequestService;
            BudgetRequest = _budgetRequestService.Get(userInfo.Id, id);
            InEditMode = false;
            IsEditable = false;
            CheckEditable();
        }
        private void CheckEditable()
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        IsEditable = true;
                    }
                    break;
                case RoleNames.Executor:
                    if (BudgetRequest.State == BudgetState.ApprovedFirstLine | BudgetRequest.State == BudgetState.Executing)
                    {
                        IsEditable = true;
                    }
                    break;
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated | BudgetRequest.State == BudgetState.PostpondDirector)
                    {
                        IsEditable = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector | BudgetRequest.State == BudgetState.PostpondFinDirector)
                    {
                        IsEditable = true;
                    }
                    break;
                case RoleNames.Requester:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        IsEditable = true;
                    }
                    break;
                default:
                    IsEditable = false;
                    break;
            }
        }

        //public void CloseRequest()
        //{
        //    BudgetRequest = null;
        //}
    }
}
