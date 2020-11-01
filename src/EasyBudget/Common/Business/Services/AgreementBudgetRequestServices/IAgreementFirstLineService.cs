using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFirstLineService
    {
        BudgetRequestUpdateOutput ApproveFirstLine(Guid userId, Guid id);
        BudgetRequestUpdateOutput RejectFirstLine(Guid userId, Guid id);
    }
}
