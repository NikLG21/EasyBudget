using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestListService
    {
        List<BudgetRequestMainListDto> GetList(Guid userId);
    }
}
