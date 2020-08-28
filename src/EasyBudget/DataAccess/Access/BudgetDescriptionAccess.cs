using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace DataAccess.Access
{
    public class BudgetDescriptionAccess: IBudgetDescriptionAccess
    {
        public void Add(BudgetDescription description)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
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
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    BudgetDescription description = context.BudgetDescriptions.AsNoTracking().FirstOrDefault(bd =>bd.Id == id);
                    if (description == null)
                    {
                        throw new EntityNotFoundException("Коментар");
                    }
                    return description;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
