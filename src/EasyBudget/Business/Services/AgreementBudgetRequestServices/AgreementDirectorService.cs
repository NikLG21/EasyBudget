using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementDirectorService : IAgreementDirectorService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        //TODO: It is not used. Done

        public AgreementDirectorService(IBudgetRequestAccess budgetRequestAccess)
        {
            _budgetRequestAccess = budgetRequestAccess;
        }

        public BudgetRequestUpdateOutput ApproveByDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ExecutorEstimated | request.State == BudgetState.PostponedDirector)
                {
                    request.State = BudgetState.ApprovedDirector;
                    request.DateDirectorApprove = DateTime.Today;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request,"Запит було успішно затверджено");
                }

                return new BudgetRequestUpdateOutput(request, "Не вдалося затвердити запит");
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
        public BudgetRequestUpdateOutput RejectByDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ExecutorEstimated | request.State == BudgetState.PostponedDirector)
                {
                    request.State = BudgetState.RejectedDirector;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request, "Запит було успішно відхилено");
                }

                return new BudgetRequestUpdateOutput(request, "Відхилити неможливо");
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

        public BudgetRequestUpdateOutput PostponedByDirector(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ExecutorEstimated)
                {
                    request.State = BudgetState.PostponedDirector;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request,"Запит було призупинено");
                }
                else
                {
                    return new BudgetRequestUpdateOutput(request,"Призупинити неможливо запит");
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
