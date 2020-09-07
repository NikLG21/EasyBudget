using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IBudgetRequestQueries1
    {
        List<BudgetRequestMainListDto> GetAllRequestDirector(DateTime from);
        List<BudgetRequestMainListDto> GetAllRequestRequester(Guid userId, DateTime from);
        List<BudgetRequestMainListDto> GetAllRequestApprover(Unit unit, DateTime from);
        List<BudgetRequestMainListDto> GetAllRequestExecutor(Role role, DateTime from);
    }
}
