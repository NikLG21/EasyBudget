using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFinDirectorService
    {
        void PostponedFinDirector(Guid userId, Guid id);
        void ExecutionStartedFinDirector(Guid userId, Guid id, DateTime? deadline);
        void ExecutionFinishedFinDirector(Guid userId, Guid id);
    }
}
