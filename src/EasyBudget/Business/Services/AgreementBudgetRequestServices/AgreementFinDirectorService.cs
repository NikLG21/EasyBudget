using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementFinDirectorService : IAgreementFinDirectorService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;
        public AgreementFinDirectorService(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }
        public void PostponedFinDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedDirector)
                {
                    request.State = BudgetState.PostpondFinDirector;
                    _budgetRequestAccess.Update(request);
                }
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public void ExecutionStartedFinDirector(Guid userId, Guid id, DateTime deadline)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedDirector | request.State == BudgetState.PostpondFinDirector)
                {
                    request.State = BudgetState.Executing;
                    request.DateDeadlineExecution = deadline;
                    request.DateStartExecution = DateTime.Today;
                    _budgetRequestAccess.Update(request);
                }
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }

        }

        public void ExecutionFinishedFinDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.Executing)
                {
                    request.State = BudgetState.Executed;
                    request.DateEndExecution = DateTime.Today;
                    _budgetRequestAccess.Update(request);
                }
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }

        }

        //public BudgetRequestUpdateOutput ExecutionStartedListFinDirector(List<Guid> requestsIds)
        //{
        //    List<BudgetRequestMainListDto> budgetRequests = _budgetRequestListQueries.GetListByIds(requestsIds);
        //    BudgetRequestUpdateOutput output = new BudgetRequestUpdateOutput();
        //    List<Guid> ids = new List<Guid>();
        //    foreach (BudgetRequestMainListDto request in budgetRequests)
        //    {
        //        if (request.State == BudgetState.ApprovedDirector | request.State == BudgetState.PostpondFinDirector)
        //        {
        //            request.State = BudgetState.Executing;
        //            output.SuccessUpdatedBudgetRequests.Add(request);
        //            ids.Add(request.Id);
        //        }
        //        else
        //        {
        //            output.SuccessUpdatedBudgetRequests.Add(request);
        //            output.Messages.Add("\"" + request.Name + "\": неможливо затвердити. Запит був видалений або змінений");
        //        }
        //    }
        //    _budgetRequestAccess.UpdateList(ids, BudgetState.Executing);
        //    return output;
        //}
    }
}
