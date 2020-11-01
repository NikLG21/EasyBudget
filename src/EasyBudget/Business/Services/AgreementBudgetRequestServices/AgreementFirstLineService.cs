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
    public class AgreementFirstLineService : IAgreementFirstLineService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;
        public AgreementFirstLineService(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }

        public BudgetRequestUpdateOutput ApproveFirstLine(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.Requested)
                {
                    request.State = BudgetState.ApprovedFirstLine;
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

        public BudgetRequestUpdateOutput RejectFirstLine(Guid userId, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.Requested)
                {
                    request.State = BudgetState.RejectedFirstLine;
                    _budgetRequestAccess.Update(request);
                    return new BudgetRequestUpdateOutput(request, "Запит успішно відхилено");
                }
                else
                {
                    return new BudgetRequestUpdateOutput(request, "Неможливо відхилити");
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
