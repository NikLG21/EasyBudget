using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetRequestListQueries
    {
        List<BudgetRequestMainListDto> GetAllDirectorRequests(DateTime from);
        List<BudgetRequestMainListDto> GetAllRequesterRequests(Guid userId, DateTime from);
        List<BudgetRequestMainListDto> GetAllApproverRequests(Guid unitId, DateTime from);
        List<BudgetRequestMainListDto> GetAllExecutorRequests(Guid departmentId, DateTime from);
        List<BudgetRequestMainListDto> GetListByIds(List<Guid> ids);
    }
}
