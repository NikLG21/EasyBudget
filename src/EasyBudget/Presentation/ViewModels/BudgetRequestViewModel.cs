using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Enums;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestViewModel : IBudgetRequestViewModel
    {
        private readonly IBudgetRequestService _budgetRequestService;
        private readonly IAgreementServiceFactory _agreementServiceFactory;

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
        public BudgetRequest ChangedBudgetRequest { get; set; }

        public FieldsStates NameField { get; set; }
        public FieldsStates DateRequestedDeadlineField { get; set; }
        public FieldsStates DateDeadlineExecutionField { get; set; }
        public FieldsStates EstimatedPriceField { get; set; }
        public FieldsStates RealPriceField { get; set; }

        public bool IsEditable { get; set; }
        public bool InEditMode { get; set; }
        public bool ApproveAble { get; set; }
        

        public BudgetRequestViewModel(Guid id,
            IBudgetRequestService budgetRequestService,
            IAgreementServiceFactory agreementServiceFactory)
        {
            _budgetRequestService = budgetRequestService;
            _agreementServiceFactory = agreementServiceFactory;
            BudgetRequest = _budgetRequestService.Get(userInfo.Id, id);
            ChangedBudgetRequest = BudgetRequest;
            ExistedRequestMode();
        }
        public BudgetRequestViewModel(IBudgetRequestService budgetRequestService,
            IAgreementServiceFactory agreementServiceFactory)
        {
            _budgetRequestService = budgetRequestService;
            _agreementServiceFactory = agreementServiceFactory;
            BudgetRequest = new BudgetRequest();
            NewRequestMode();
        }


        public void ChangeMode()
        {
            if (IsEditable == true)
            {
                InEditMode = true;
                FieldStatesEditMode();
            }
        }
        public void ApproveRequest()
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    _agreementServiceFactory.GetFirstLine().ApproveFirstLine(userInfo.Id,BudgetRequest.Id);
                    break;
                case RoleNames.Director:
                    _agreementServiceFactory.GetDirector().ApproveDirector(userInfo.Id,BudgetRequest.Id);
                    break;
                case RoleNames.FinDirector:
                    _agreementServiceFactory.GetFinDirector().ExecutionStartedFinDirector(userInfo.Id,BudgetRequest.Id,ChangedBudgetRequest.DateDeadlineExecution);
                    break;
                case RoleNames.Executor:
                    if (ChangedBudgetRequest.RealPrice != null)
                    {
                        _agreementServiceFactory.GetExecutor().RealPriceAdded(userInfo.Id, BudgetRequest.Id, ChangedBudgetRequest.RealPrice);
                    } 
                    break;
            }
        }

        public void CreateNewRequest()
        {
            if (ChangedBudgetRequest.Name != null)
            {
                _budgetRequestService.AddRequest(userInfo.Id, ChangedBudgetRequest);
            }
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
        private void CheckApproveAble()
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        ApproveAble = true;
                    }
                    break;
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated |
                        BudgetRequest.State == BudgetState.PostpondDirector)
                    {
                        ApproveAble = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector | BudgetRequest.State == BudgetState.PostpondFinDirector)
                    {
                        ApproveAble = true;
                    }
                    break;
                case RoleNames.Executor:
                    if (BudgetRequest.State == BudgetState.ApprovedFirstLine)
                    {
                        ApproveAble = true;
                    }
                    break;
                
            }
        }
        private void FieldStatesEditMode()
        {

            switch (role.Name)
            {
                case RoleNames.Executor:
                    if (BudgetRequest.State == BudgetState.ApprovedFirstLine)
                    {
                        RealPriceField = FieldsStates.UnChanged;
                    }
                    break;
                case RoleNames.Approver:
                case RoleNames.Director:
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector | BudgetRequest.State == BudgetState.PostpondFinDirector)
                    {
                        DateDeadlineExecutionField = FieldsStates.UnChanged;
                    }
                    break;
                case RoleNames.Requester:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        NameField = FieldsStates.UnChanged;
                        DateRequestedDeadlineField = FieldsStates.UnChanged;
                        EstimatedPriceField = FieldsStates.NotEditable;
                    }
                    break;

            }
        }

        private void NewRequestMode()
        {
            InEditMode = true;
            IsEditable = false;
            ApproveAble = false;
            NameField = FieldsStates.UnChanged;
            DateRequestedDeadlineField = FieldsStates.UnChanged;
            DateDeadlineExecutionField = FieldsStates.NotEditable;
            RealPriceField = FieldsStates.NotEditable;
            EstimatedPriceField = FieldsStates.UnChanged;
        }

        private void ExistedRequestMode()
        {
            InEditMode = false;
            IsEditable = false;
            ApproveAble = false;
            CheckEditable();
            CheckApproveAble();
            NameField = FieldsStates.NotEditable;
            DateRequestedDeadlineField = FieldsStates.NotEditable;
            DateDeadlineExecutionField = FieldsStates.NotEditable;
            RealPriceField = FieldsStates.NotEditable;
            EstimatedPriceField = FieldsStates.NotEditable;
        }
    }
}
