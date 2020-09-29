using System;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace DataAccess.Access
{
    public class BudgetDescriptionAccess: IBudgetDescriptionAccess
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetDescriptionAccess(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Add(BudgetDescription description)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    context.BudgetDescriptions.Add(description);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public BudgetDescription Get(Guid id)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    BudgetDescription budgetDescription = context.BudgetDescriptions.AsNoTracking().FirstOrDefault(bd =>bd.Id == id);
                    if (budgetDescription == null)
                    {
                        throw new EntityNotFoundException("Коментар");
                    }
                    return budgetDescription;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
