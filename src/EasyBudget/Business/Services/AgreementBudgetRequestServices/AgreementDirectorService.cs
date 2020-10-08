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
    public class AgreementDirectorService : IAgreementDirectorService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;
        public AgreementDirectorService(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }
        public void ApproveDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ExecutorEstimated | request.State == BudgetState.PostpondDirector)
                {
                    request.State = BudgetState.ApprovedDirector;
                    request.DateDirectorApprove = DateTime.Today;
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
        public void RejectDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ExecutorEstimated | request.State == BudgetState.PostpondDirector)
                {
                    request.State = BudgetState.RejectedDirector;
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
        public void PostponedDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ExecutorEstimated)
                {
                    request.State = BudgetState.PostpondDirector;
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
    }
}
