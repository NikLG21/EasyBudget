using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace DataAccess.Access
{
    public class BudgetRequestAccess : IBudgetRequestAccess
    {
        public void Add(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    context.BudgetRequests.Add(request);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public void Update(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    context.BudgetRequests.Add(request);
                    context.Entry(request).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public void Delete(Guid Id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    BudgetRequest request = context.BudgetRequests.FirstOrDefault(e => e.Id == Id);
                    if (request != null)
                    {
                        context.BudgetRequests.Attach(request);
                        context.BudgetRequests.Remove(request);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public BudgetRequest Get(Guid Id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    BudgetRequest request = context.BudgetRequests.AsNoTracking().FirstOrDefault(e => e.Id == Id);
                    if (request == null)
                    {
                        throw new EntityNotFoundException("Запит");
                    }
                    return request;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }

            }
        }
    }
}
