using System;
using EasyBudget.Common.Business.Outputs;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementDirectorService
    {
        BudgetRequestUpdateOutput ApproveByDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput RejectByDirector(Guid userId, Guid id);
        BudgetRequestUpdateOutput PostponedByDirector(Guid userId, Guid id);
    }
}
