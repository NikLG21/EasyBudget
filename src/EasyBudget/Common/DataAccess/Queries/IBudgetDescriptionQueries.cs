using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetDescriptionQueries
    {
        List<BudgetDesctiptionMainListDto> GetBudgetDescriptionByRequest(Guid budgetRequestId);
    }
}
