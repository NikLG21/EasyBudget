using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;

namespace DataAccessCore.Queries
{
    public class BudgetDescriptionQueriesCore : IBudgetDescriptionQueries
    {
        private readonly BudgetRequestDbContextCoreFactory _factory;

        public BudgetDescriptionQueriesCore(BudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }

        public List<BudgetDescriptionMainListDto> GetBudgetDescriptionsByRequest(Guid budgetRequestId)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    List<BudgetDescriptionMainListDto> list = context
                        .BudgetDescriptions
                        .Where(bd => bd.BudgetRequest.Id == budgetRequestId)
                        .Select(bd =>
                            new BudgetDescriptionMainListDto(bd.Id)
                            {
                                UserName = bd.User.Name,
                                Date = bd.Date,
                                Description = bd.Description,
                                BudgetRequestId = bd.BudgetRequest.Id

                            }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
