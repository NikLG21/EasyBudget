using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetRequestQueries
    {
        List<BudgetRequestMainListDto> GetBudgetRequestsByRequestor(Guid userId, DateTime from, DateTime to);
        List<BudgetRequestMainListDto> GetBudgetRequestsByApprover(Guid userId, DateTime from, DateTime to);
        List<BudgetRequestMainListDto> GetBudgetRequestByExecutor(Guid userId, DateTime from, DateTime to);
        List<BudgetRequestMainListDto> GetBudgetRequestByTime(DateTime from, DateTime to);
        List<BudgetRequestMainListDto> GetBudgetRequestUnapprovedDirectors(BudgetState state);
        List<BudgetRequestMainListDto> GetBudgetRequestUnapprovedApprover(Guid userId);
        List<BudgetRequestMainListDto> GetBudgetRequestUncheckedExecutor(Department department);
    }
}
