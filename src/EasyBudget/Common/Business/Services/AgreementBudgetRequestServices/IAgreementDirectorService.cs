using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementDirectorService
    {
        void ApproveDirector(Guid userId, Guid id);
        void RejectDirector(Guid userId, Guid id);
        void PostponedDirector(Guid userId, Guid id);
    }
}
