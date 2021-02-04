using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementFinDirectorService : IAgreementFinDirectorService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        public AgreementFinDirectorService(IBudgetRequestAccess budgetRequestAccess)
        {
            _budgetRequestAccess = budgetRequestAccess;
        }

        public BudgetRequestUpdateOutput PostponedByFinDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedDirector)
                {
                    request.State = BudgetState.PostponedFinDirector;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request, "Запит було успішно призупинено");
                }
                //TODO: Please remove else. Done
                return new BudgetRequestUpdateOutput(request, "Неможливо призупинити запит");
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

        public BudgetRequestUpdateOutput ExecutionStartedByFinDirector(Guid userId, Guid id, DateTime? deadline)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedDirector | request.State == BudgetState.PostponedFinDirector)
                {
                    request.State = BudgetState.Executing;
                    request.DateDeadlineExecution = deadline;
                    request.DateStartExecution = DateTime.Today;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request, "Запит успішно затверджено");
                }

                //TODO: Please remove else. Done
                return new BudgetRequestUpdateOutput(request, "Неможливо затвердити");
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

        public BudgetRequestUpdateOutput ExecutionFinishedByFinDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.Executing)
                {
                    request.State = BudgetState.Executed;
                    request.DateEndExecution = DateTime.Today;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request, "Виконання запиту успішно завершено");
                }
                //TODO: Please remove else. Done
                return new BudgetRequestUpdateOutput(request, "Неможливо завершити виконання запиту");
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
    }
}
