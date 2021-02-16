using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementExecutorService : IAgreementExecutorService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;

        public AgreementExecutorService(IBudgetRequestAccess budgetRequestAccess)
        {
            _budgetRequestAccess = budgetRequestAccess;
        }
        public BudgetRequestUpdateOutput AddRealPrice(UserMainInfoDto userMainInfo, Guid id, decimal? realPrice)
        {
            try
            {
                BudgetRequest request = _budgetRequestAccess.Get(id);
                if (request.State == BudgetState.ApprovedFirstLine)
                {
                    request.State = BudgetState.ExecutorEstimated;
                    request.RealPrice = realPrice;
                    request.ExecutorId = userMainInfo.Id;
                    _budgetRequestAccess.Update(request);
                    request.Executor = new User(userMainInfo.Id)
                    {
                        Name = userMainInfo.Name
                    };
                    return new BudgetRequestUpdateOutput(request,"Запит був оновлений та затверджений");
                }

                return new BudgetRequestUpdateOutput(request, "Неможливо затвердити запит");
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
