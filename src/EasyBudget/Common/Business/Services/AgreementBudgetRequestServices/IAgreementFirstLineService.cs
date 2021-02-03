using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFirstLineService
    {
        BudgetRequestUpdateOutput ApproveByFirstLine(UserMainInfoDto userMainInfo, Guid id);
        BudgetRequestUpdateOutput RejectByFirstLine(UserMainInfoDto userMainInfo, Guid id);
    }
}
