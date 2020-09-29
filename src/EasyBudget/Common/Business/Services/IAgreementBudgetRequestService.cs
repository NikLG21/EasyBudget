using System;
using System.Collections.Generic;
using EasyBudget.Common.Utils;

namespace EasyBudget.Common.Business.Services
{
    //TODO: Very big service. Please, split it into several. E.g per role
    public interface IAgreementBudgetRequestService
    {
        void ApproveFirstLine(Guid userId, Guid id);
        Output ApproveListFirstLine(List<Guid> requestsIds);
        void ApproveDirector(Guid userId, Guid id);
        Output ApproveListDirector(List<Guid> requestsIds);
        Output ExecutionStartedListFinDirector(List<Guid> requestsIds);
        void RejectFirstLine(Guid userId, Guid id);
        void RejectDirector(Guid userId, Guid id);
        void PostponedDirector(Guid userId, Guid id);
        void PostponedFinDirector(Guid userId, Guid id);
        void RealPriceAdded(Guid userId, Guid id, decimal realPrice);
        void ExecutionStartedFinDirector(Guid userId, Guid id, DateTime deadline);
        void ExecutionFinishedFinDirector(Guid userId, Guid id);
    }
}
