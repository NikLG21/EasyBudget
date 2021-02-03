using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementDirectorService
    {
        //TODO: e.g. ApproveByDirector ...
        BudgetRequestUpdateOutput ApproveDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput RejectDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput PostponedDirector(Guid userId, Guid id);
    }
}
