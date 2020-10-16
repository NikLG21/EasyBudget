using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetDescriptionQueries
    {
        List<BudgetDescriptionMainListDto> GetBudgetDescriptionsByRequest(Guid budgetRequestId);
    }
}
