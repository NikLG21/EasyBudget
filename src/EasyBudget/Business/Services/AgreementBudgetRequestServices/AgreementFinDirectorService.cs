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
        public BudgetRequestUpdateOutput PostponedFinDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedDirector)
                {
                    request.State = BudgetState.PostpondFinDirector;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request, "Запит було успішно відкладено");
                }
                else
                {
                    return new BudgetRequestUpdateOutput(request, "Неможливо відкласти запит");
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

        public BudgetRequestUpdateOutput ExecutionStartedFinDirector(Guid userId, Guid id, DateTime? deadline)
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
                    return new BudgetRequestUpdateOutput(request, "Запит успішно затверджено");
                }
                else
                {
                    return new BudgetRequestUpdateOutput(request, "Неможливо затвердити");
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

        public BudgetRequestUpdateOutput ExecutionFinishedFinDirector(Guid userId, Guid id)
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
                else
                {
                    return new BudgetRequestUpdateOutput(request, "Неможливо завершити виконання запиту");
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
    }
}
