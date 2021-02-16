using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementFirstLineService : IAgreementFirstLineService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        //TODO: It is not used
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;
        public AgreementFirstLineService(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }

        public BudgetRequestUpdateOutput ApproveByFirstLine(UserMainInfoDto userMainInfo, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.Requested)
                {
                    request.State = BudgetState.ApprovedFirstLine;
                    request.ApproverId = userMainInfo.Id;
                    _budgetRequestAccess.Update(request);
                    request.Approver = new User(userMainInfo.Id)
                    {
                        Name = userMainInfo.Name
                    };
                    return new BudgetRequestUpdateOutput(request, "Запит успішно затверджено");
                }

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

        public BudgetRequestUpdateOutput RejectByFirstLine(UserMainInfoDto userMainInfo, Guid id)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.Requested)
                {
                    request.State = BudgetState.RejectedFirstLine;
                    request.ApproverId = userMainInfo.Id;
                    _budgetRequestAccess.Update(request);
                    request.Approver = new User(userMainInfo.Id)
                    {
                        Name = userMainInfo.Name
                    };
                    return new BudgetRequestUpdateOutput(request, "Запит успішно відхилено");
                }

                return new BudgetRequestUpdateOutput(request, "Неможливо відхилити");
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
