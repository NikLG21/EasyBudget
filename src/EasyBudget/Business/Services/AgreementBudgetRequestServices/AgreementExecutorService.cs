using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementExecutorService : IAgreementExecutorService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;

        public AgreementExecutorService(IBudgetRequestAccess budgetRequestAccess)
        {
            _budgetRequestAccess = budgetRequestAccess;
        }
        public void RealPriceAdded(Guid userId, Guid id, decimal realPrice)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedFirstLine)
                {
                    request.State = BudgetState.PostpondFinDirector;
                    request.RealPrice = realPrice;
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
