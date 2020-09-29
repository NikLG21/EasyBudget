using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestListDirectorService : IBudgetRequestListService
    {
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;

        public BudgetRequestListDirectorService(IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestListQueries = budgetRequestListQueries;
        }

        public List<BudgetRequestMainListDto> GetList(UserMainInfoDto userInfo)
        {
            try
            {
                return _budgetRequestListQueries.GetAllRequestDirector(DateTime.MinValue);
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }
    }
}
