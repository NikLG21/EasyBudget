using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFinDirectorService
    {
        //TODO: PostponedByFinDirector. Done
        BudgetRequestUpdateOutput PostponedByFinDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput ExecutionStartedByFinDirector(Guid userId, Guid id, DateTime? deadline);
        BudgetRequestUpdateOutput ExecutionFinishedByFinDirector(Guid userId, Guid id);
    }
}
