using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestListDirectorService : IBudgetRequestListService
    {
        public List<BudgetRequestMainListDto> GetList(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
