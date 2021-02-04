using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    //TODO: Base is very confusing. Done
    public interface IAgreementCommonService
    {
        BudgetRequestListUpdateOutput ApproveListByRole(Guid userId, List<Guid> requestsIds,Role role);
    }
}
