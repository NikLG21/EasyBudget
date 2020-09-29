using System;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementExecutorService
    {
        void RealPriceAdded(Guid userId, Guid id, decimal realPrice);
    }
}
