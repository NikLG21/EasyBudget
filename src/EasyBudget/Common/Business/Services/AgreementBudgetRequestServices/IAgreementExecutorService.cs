using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Services.AgreementBudgetRequestServices
{
    public interface IAgreementExecutorService
    {
        //TODO: AddRealPrice. Done
        BudgetRequestUpdateOutput AddRealPrice(UserMainInfoDto userMainInfo, Guid id, decimal? realPrice);
    }
}
