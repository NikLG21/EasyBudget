using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;

namespace DataAccess.Queries
{
    public class BudgetDescriptionQueries : IBudgetDescriptionQueries
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetDescriptionQueries(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }

        public List<BudgetDescriptionMainListDto> GetBudgetDescriptionsByRequest(Guid budgetRequestId)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetDescriptionMainListDto> list = context
                        .BudgetDescriptions
                        .Where(bd => bd.BudgetRequest.Id == budgetRequestId)
                        .Select(bd =>
                            new BudgetDescriptionMainListDto
                            {
                                Id = bd.Id,
                                UserName = bd.User.Name,
                                Date = bd.Date,
                                Description = bd.Description
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
