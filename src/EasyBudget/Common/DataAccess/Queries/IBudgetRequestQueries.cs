using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetRequestQueries
    {
        List<BudgetRequestMainListDto> GetBudgetRequestsByRequestor(Guid userId);
        List<BudgetRequestMainListDto> GetBudgetRequestsByApprover(Guid userId);
        List<BudgetRequestMainListDto> GetBudgetRequestByExecutor(Guid userId);
        List<BudgetRequestMainListDto> GetBudgetRequestByTime(DateTime from, DateTime to);
    }
}
