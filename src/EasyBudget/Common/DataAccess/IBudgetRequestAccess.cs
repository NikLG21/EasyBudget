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
        //TODO: Bad function. Here we cannot use DTO. Please change signature or move to I...Command
        void UpdateList(List<BudgetRequestMainListDto> requests);
        void Delete(Guid id);
        BudgetRequest Get(Guid id);
        //TODO: Please move to another class. e.g. IBudgetRequestListQueries. Here we cannot use DTO
        List<BudgetRequestMainListDto> GetList(List<Guid> ids);

    }
}
