using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFinDirectorService
    {
        BudgetRequestUpdateOutput PostponedFinDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput ExecutionStartedFinDirector(Guid userId, Guid id, DateTime? deadline);
        BudgetRequestUpdateOutput ExecutionFinishedFinDirector(Guid userId, Guid id);
    }
}
