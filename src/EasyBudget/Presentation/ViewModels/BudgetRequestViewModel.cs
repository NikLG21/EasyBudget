using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Presentation.Enums;
using EasyBudget.Presentation.Interfaces;
using EasyBudget.Presentation.Utils;

namespace EasyBudget.Presentation.ViewModels
{
    public class BudgetRequestViewModel : IBudgetRequestViewModel
    {
        public event System.Action EntityViewModelChanged;

        private readonly IBudgetRequestService _budgetRequestService;
        private readonly IAgreementServiceFactory _agreementServiceFactory;
        private readonly IDepartmentService _departmentService;

        private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("6a875efe-05ef-4137-889a-137df8c67ab2"),
            Name = "Кирил Цибулькин",
            CurrentRoleId = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            CurrentRoleName = "Approver",
            UnitId = Guid.Parse("35f0579d-a8d6-4c6a-a241-2f4726b6a9d1"),
            UnitName = "2 Клініка"
        };

        private Role role = new Role()
        {
            Id = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            Name = "Approver",
        };

        public BudgetRequest BudgetRequest { get; set; }
        public BudgetRequest ChangedBudgetRequest { get; set; }

        public List<PairGuid> Departments { get; private set; }

        public FieldsStates NameField { get; set; }
        public FieldsStates DateRequestedDeadlineField { get; set; }
        public FieldsStates DateDeadlineExecutionField { get; set; }
        public FieldsStates EstimatedPriceField { get; set; }
        public FieldsStates RealPriceField { get; set; }

        public bool InEditMode { get; set; }

        public bool Editable { get; set; }
        public bool EditableByRequester { get; set; }
        public bool ApproveAble { get; set; }
        public bool RejectAble { get; set; }
        public bool PostponeAble { get; set; }
        public bool DeleteAble { get; set; }
        public bool NewRequestMode { get; set; }

        public BudgetRequestViewModel(Guid id,
            IBudgetRequestService budgetRequestService,
            IAgreementServiceFactory agreementServiceFactory)
        {
            _budgetRequestService = budgetRequestService;
            _agreementServiceFactory = agreementServiceFactory;
            BudgetRequest = _budgetRequestService.GetRequest(userInfo.Id, id);
            ChangedBudgetRequest = BudgetRequest;
            ExistedRequestMode();
        }
        public BudgetRequestViewModel(IBudgetRequestService budgetRequestService,
            IAgreementServiceFactory agreementServiceFactory,
            IDepartmentService departmentService)
        {
            _budgetRequestService = budgetRequestService;
            _agreementServiceFactory = agreementServiceFactory;
            _departmentService = departmentService;
            BudgetRequest = new BudgetRequest();
            BudgetRequest.Department = new Department();
            BudgetRequest.Department.Id= Guid.Empty;
            ChangedBudgetRequest = BudgetRequest;
            NewRequestModeStart();
        }


        public void ChangeEditMode()
        {
            if (Editable == true)
            {
                InEditMode = true;
                FieldStatesEditMode();
            }
        }

        public void ApproveRequest()
        {
            BudgetRequestUpdateOutput output;
            switch (role.Name)
            {
                case RoleNames.Approver:
                    output = _agreementServiceFactory.GetFirstLine().ApproveByFirstLine(userInfo,BudgetRequest.Id);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
                case RoleNames.Director:
                    output = _agreementServiceFactory.GetDirector().ApproveByDirector(userInfo.Id,BudgetRequest.Id);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
                case RoleNames.FinDirector:
                    output = _agreementServiceFactory.GetFinDirector().ExecutionStartedByFinDirector(userInfo.Id,BudgetRequest.Id,ChangedBudgetRequest.DateDeadlineExecution);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
                case RoleNames.Executor:
                    if (ChangedBudgetRequest.RealPrice != null)
                    {
                        output = _agreementServiceFactory.GetExecutor().AddRealPrice(userInfo, BudgetRequest.Id, ChangedBudgetRequest.RealPrice);
                        BudgetRequest = output.Request;
                        ChangedBudgetRequest = output.Request;
                    } 
                    break;
            }
            ExistedRequestMode();
            EntityViewModelChanged?.Invoke();
        }

        public void CreateNewRequest()
        {
            if (role.Name != RoleNames.Requester)
            {
                return;
            }
            if (ChangedBudgetRequest.Name != null|ChangedBudgetRequest.Department.Id!=Guid.Empty)
            {
                BudgetRequestUpdateOutput output = _budgetRequestService.AddRequest(userInfo.Id, ChangedBudgetRequest);
                BudgetRequest = output.Request;
                ChangedBudgetRequest = output.Request;
                ExistedRequestMode();
            }
            else
            {
                if (ChangedBudgetRequest.Name == null)
                {
                    throw new LackMandatoryInformation("Назва");
                }

                if (ChangedBudgetRequest.Department.Id == Guid.Empty)
                {
                    throw new LackMandatoryInformation("Відділ");
                }
                
            }
        }

        public void CancelChanges()
        {
            ChangedBudgetRequest = BudgetRequest;
            FieldStatesEditMode();
        }

        public void RejectRequest()
        {
            BudgetRequestUpdateOutput output;
            switch (role.Name)
            {
                case RoleNames.Approver:
                    output = _agreementServiceFactory.GetFirstLine().RejectByFirstLine(userInfo, BudgetRequest.Id);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
                case RoleNames.Director:
                    output = _agreementServiceFactory.GetDirector().RejectByDirector(userInfo.Id, BudgetRequest.Id);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
            }
            ExistedRequestMode();
            EntityViewModelChanged?.Invoke();
        }

        public void PostponeRequest()
        {
            BudgetRequestUpdateOutput output;
            switch (role.Name)
            {
                case RoleNames.FinDirector:
                    output = _agreementServiceFactory.GetFinDirector().PostponedByFinDirector(userInfo.Id, BudgetRequest.Id);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
                case RoleNames.Director:
                    output = _agreementServiceFactory.GetDirector().PostponedByDirector(userInfo.Id, BudgetRequest.Id);
                    BudgetRequest = output.Request;
                    ChangedBudgetRequest = output.Request;
                    break;
            }
            ExistedRequestMode();
            EntityViewModelChanged?.Invoke();
        }

        public void DeleteRequestByRequester()
        {
            if (role.Name != RoleNames.Requester && BudgetRequest.State!= BudgetState.Requested)
            {
                return;
            }
            BudgetRequestUpdateOutput output =  _budgetRequestService.DeleteRequest(userInfo,BudgetRequest);
            BudgetRequest = output.Request;
            ChangedBudgetRequest = output.Request;
            ExistedRequestMode();
            EntityViewModelChanged?.Invoke();
        }

        public void EditByRequester()
        {
            if (role.Name != RoleNames.Requester)
            {
                return;
            }
            BudgetRequestUpdateOutput output = _budgetRequestService.UpdateRequestByRequester(userInfo, ChangedBudgetRequest);
            BudgetRequest = output.Request;
            ChangedBudgetRequest = output.Request;
            ExistedRequestMode();
            EntityViewModelChanged?.Invoke();
        }

        private void CheckEditable()
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        Editable = true;
                    }
                    break;
                case RoleNames.Executor:
                    if (BudgetRequest.State == BudgetState.ApprovedFirstLine | BudgetRequest.State == BudgetState.Executing)
                    {
                        Editable = true;
                    }
                    break;
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated | BudgetRequest.State == BudgetState.PostponedDirector)
                    {
                        Editable = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector | BudgetRequest.State == BudgetState.PostponedFinDirector)
                    {
                        Editable = true;
                    }
                    break;
                case RoleNames.Requester:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        Editable = true;
                        EditableByRequester = true;
                    }
                    break;
                default:
                    Editable = false;
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
                        BudgetRequest.State == BudgetState.PostponedDirector)
                    {
                        ApproveAble = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector | BudgetRequest.State == BudgetState.PostponedFinDirector)
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

        private void CheckRejectAble()
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        RejectAble = true;
                    }
                    break;
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated |
                        BudgetRequest.State == BudgetState.PostponedDirector)
                    {
                        RejectAble = true;
                    }
                    break;
            }
        }

        private void CheckPostponedAble()
        {
            switch (role.Name)
            {
                case RoleNames.Director:
                    if (BudgetRequest.State == BudgetState.ExecutorEstimated)
                    {
                        PostponeAble = true;
                    }
                    break;
                case RoleNames.FinDirector:
                    if (BudgetRequest.State == BudgetState.ApprovedDirector)
                    {
                        PostponeAble = true;
                    }
                    break;
            }
        }

        private void CheckDeleteAble()
        {
            if (role.Name == RoleNames.Requester && BudgetRequest.State == BudgetState.Requested)
            {
                DeleteAble = true;
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
                    if (BudgetRequest.State == BudgetState.ApprovedDirector | BudgetRequest.State == BudgetState.PostponedFinDirector)
                    {
                        DateDeadlineExecutionField = FieldsStates.UnChanged;
                    }
                    break;
                case RoleNames.Requester:
                    if (BudgetRequest.State == BudgetState.Requested)
                    {
                        NameField = FieldsStates.UnChanged;
                        DateRequestedDeadlineField = FieldsStates.UnChanged;
                        EstimatedPriceField = FieldsStates.UnChanged;
                    }
                    break;

            }
        }

        private void NewRequestModeStart()
        {
            
            Departments= new List<PairGuid>();
            Departments.AddRange(_departmentService.GetAllDepartments()
                .Select(br=>new PairGuid(br.Id,br.Name))
                .ToList());
            NewRequestMode = true;
            InEditMode = true;
            Editable = false;
            ApproveAble = false;
            RejectAble = false;
            PostponeAble = false;
            DeleteAble = false;
            EditableByRequester = false;
            NameField = FieldsStates.UnChanged;
            DateRequestedDeadlineField = FieldsStates.UnChanged;
            DateDeadlineExecutionField = FieldsStates.NotEditable;
            RealPriceField = FieldsStates.NotEditable;
            EstimatedPriceField = FieldsStates.UnChanged;
        }

        private void ExistedRequestMode()
        {
            NewRequestMode = false;
            InEditMode = false;
            Editable = false;
            ApproveAble = false;
            RejectAble = false;
            PostponeAble = false;
            DeleteAble = false;
            EditableByRequester = false;
            if (BudgetRequest.State != BudgetState.Undefined)
            {
                CheckEditable();
                CheckApproveAble();
                CheckRejectAble();
                CheckPostponedAble();
                CheckDeleteAble();
            }
            NameField = FieldsStates.NotEditable;
            DateRequestedDeadlineField = FieldsStates.NotEditable;
            DateDeadlineExecutionField = FieldsStates.NotEditable;
            RealPriceField = FieldsStates.NotEditable;
            EstimatedPriceField = FieldsStates.NotEditable;
        }

        
    }
}
