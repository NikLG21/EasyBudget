using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementFirstLineService
    {
        //TODO: ApproveByFirstLine. Done
        BudgetRequestUpdateOutput ApproveByFirstLine(UserMainInfoDto userMainInfo, Guid id);
        BudgetRequestUpdateOutput RejectByFirstLine(UserMainInfoDto userMainInfo, Guid id);
    }
}
