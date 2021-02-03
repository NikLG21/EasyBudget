using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.DataAccess.Commands;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace DataAccessCore.Commands
{
    public class BudgetRequestCommandsCore : IBudgetRequestCommands
    {
        private readonly IBudgetRequestDbContextCoreFactory _factory;

        public BudgetRequestCommandsCore(IBudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }

        public void UpdateList(List<Guid> ids, BudgetState newState, Guid userId)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    List<BudgetRequest> budgetRequests = context.BudgetRequests.Where(br => ids.Contains(br.Id)).ToList();
                    if (newState == BudgetState.ApprovedFirstLine)
                    {
                        budgetRequests.ForEach(br =>
                        {
                            br.State = newState;
                            br.ApproverId = userId;
                        });
                    }
                    else
                    {
                        budgetRequests.ForEach(br => br.State = newState);
                    }
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
