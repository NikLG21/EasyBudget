using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementBaseService
    {
        BudgetRequestListUpdateOutput ApproveListByRole(Guid userId,List<Guid> requestsIds,Role role);
    }
}
