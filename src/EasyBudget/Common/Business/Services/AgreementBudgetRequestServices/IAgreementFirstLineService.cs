using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFirstLineService
    {
        //TODO: ApproveByFirstLine
        BudgetRequestUpdateOutput ApproveFirstLine(UserMainInfoDto userMainInfo, Guid id);
        BudgetRequestUpdateOutput RejectFirstLine(UserMainInfoDto userMainInfo, Guid id);
    }
}
