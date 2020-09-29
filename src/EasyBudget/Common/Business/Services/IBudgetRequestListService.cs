using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestListService
    {
        List<BudgetRequestMainListDto> GetList(UserMainInfoDto userInfo);
    }
}
