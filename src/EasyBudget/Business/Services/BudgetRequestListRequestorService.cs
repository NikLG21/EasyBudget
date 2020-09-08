using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestListRequestorService : IBudgetRequestListService
    {
        private IBudgetRequestListQueries _budgetRequestListQueries;

        public BudgetRequestListRequestorService(IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestListQueries = budgetRequestListQueries;
        }

        public List<BudgetRequestMainListDto> GetList(UserMainInfoDto userInfo)
        {
            try
            {
                return _budgetRequestListQueries.GetAllRequestRequester(userInfo.Id,DateTime.MinValue);
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
