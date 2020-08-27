using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IAgreementBudgetRequestService
    {
        void ApproveFirstLine(Guid id);
        void ApproveDirector(Guid id);
        void RejectFirstLine(Guid id);
        void RejectDirector(Guid id);
        void PostponedDirector(Guid id);
        void PostponedFinDirector(Guid id);
        void RealPriceAdded(Guid id, decimal realPrice);
        void SpecifyBudgetRequest(Guid id,User user);
    }
}
