using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IBudgetRequestAccess
    {
        void Add(BudgetRequest request);
        void Update(BudgetRequest request);
        void UpdateList(List<BudgetRequestMainListDto> requests);
        void Delete(Guid id);
        BudgetRequest Get(Guid id);
        List<BudgetRequestMainListDto> GetList(List<Guid> ids);

    }
}
