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
        public List<BudgetDesctiptionMainListDto> GetBudgetDescriptionByRequest(Guid budgetRequestId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    List<BudgetDesctiptionMainListDto> list = context
                        .BudgetDescriptions
                        .Where(bd => bd.BudgetRequest.Id == budgetRequestId)
                        .Select(bd =>
                            new BudgetDesctiptionMainListDto
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
