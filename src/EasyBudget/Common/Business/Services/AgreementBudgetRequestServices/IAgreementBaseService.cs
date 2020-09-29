using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementBaseService
    {
        BudgetRequestUpdateOutput ApproveListByRole(List<Guid> requestsIds,Role role);
    }
}
