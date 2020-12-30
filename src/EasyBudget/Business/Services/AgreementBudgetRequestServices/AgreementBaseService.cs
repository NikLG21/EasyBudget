using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementBaseService : IAgreementBaseService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;
        
        public AgreementBaseService(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }
        public BudgetRequestListUpdateOutput ApproveListByRole(Guid userId, List<Guid> requestsIds, Role role)
        {
            List<BudgetRequestMainListDto> budgetRequests = _budgetRequestListQueries.GetListByIds(requestsIds);
            BudgetRequestListUpdateOutput output = new BudgetRequestListUpdateOutput();
            List<Guid> ids = new List<Guid>();
            switch (role.Name)
            {
                case RoleNames.Approver:
                    foreach (BudgetRequestMainListDto request in budgetRequests)
                    {
                        if (request.State == BudgetState.Requested)
                        {
                            request.State = BudgetState.ApprovedFirstLine;
                            output.SuccessUpdatedBudgetRequests.Add(request);
                            ids.Add(request.Id);
                        }
                        else
                        {
                            output.SuccessUpdatedBudgetRequests.Add(request);
                            output.Messages.Add("\"" + request.Name + "\": неможливо затвердити. Запит був видалений або змінений");
                        }
                    }
                    _budgetRequestAccess.UpdateList(ids, BudgetState.ApprovedFirstLine,userId);
                    break;
                case RoleNames.Director:
                    foreach (BudgetRequestMainListDto request in budgetRequests)
                    {
                        if (request.State == BudgetState.ExecutorEstimated | request.State == BudgetState.PostponedDirector)
                        {
                            request.State = BudgetState.ApprovedDirector;
                            output.SuccessUpdatedBudgetRequests.Add(request);
                            ids.Add(request.Id);
                        }
                        else
                        {
                            output.SuccessUpdatedBudgetRequests.Add(request);
                            output.Messages.Add("\"" + request.Name + "\": неможливо затвердити. Запит був видалений або змінений");
                        }
                    }
                    _budgetRequestAccess.UpdateList(ids, BudgetState.ApprovedDirector,userId);
                    break;
                case RoleNames.FinDirector:
                    foreach (BudgetRequestMainListDto request in budgetRequests)
                    {
                        if (request.State == BudgetState.ApprovedDirector | request.State == BudgetState.PostponedFinDirector)
                        {
                            request.State = BudgetState.Executing;
                            output.SuccessUpdatedBudgetRequests.Add(request);
                            ids.Add(request.Id);
                        }
                        else
                        {
                            output.SuccessUpdatedBudgetRequests.Add(request);
                            output.Messages.Add("\"" + request.Name + "\": неможливо затвердити. Запит був видалений або змінений");
                        }
                    }
                    _budgetRequestAccess.UpdateList(ids, BudgetState.Executing,userId);
                    break;
                default:
                    break;
            }
            return output;
        }
    }
}
