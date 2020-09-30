using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFirstLineService
    {
        void ApproveFirstLine(Guid userId, Guid id);
        void RejectFirstLine(Guid userId, Guid id);
    }
}
