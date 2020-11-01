using System;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementExecutorService
    {
        BudgetRequestUpdateOutput RealPriceAdded(Guid userId, Guid id, decimal? realPrice);
    }
}
