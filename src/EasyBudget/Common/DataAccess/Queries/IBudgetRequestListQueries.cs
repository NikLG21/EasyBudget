using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetRequestListQueries
    {
        List<BudgetRequestMainListDto> GetAllRequestDirector(DateTime from);
        List<BudgetRequestMainListDto> GetAllRequestRequester(Guid userId, DateTime from);
        List<BudgetRequestMainListDto> GetAllRequestApprover(Guid unitId, DateTime from);
        List<BudgetRequestMainListDto> GetAllRequestExecutor(Guid departmentId, DateTime from);
    }
}
