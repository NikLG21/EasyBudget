using System;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFinDirectorService
    {
        BudgetRequestUpdateOutput PostponedByFinDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput ExecutionStartedByFinDirector(Guid userId, Guid id, DateTime? deadline);
        BudgetRequestUpdateOutput ExecutionFinishedByFinDirector(Guid userId, Guid id);
    }
}
