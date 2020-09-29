using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace DataAccess.Access
{
    public class BudgetRequestAccess : IBudgetRequestAccess
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetRequestAccess(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Add(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = _factory.Create())
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
            using (BudgetRequestDbContext context = _factory.Create())
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

        public void UpdateList(List<Guid> ids, BudgetState newState)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequest> budgetRequests = context.BudgetRequests.Where(br => ids.Contains(br.Id)).ToList();
                    budgetRequests.ForEach(br => br.State = newState);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public void Delete(Guid id)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    BudgetRequest request = context.BudgetRequests.FirstOrDefault(e => e.Id == id);
                    if (request != null)
                    {
                        context.BudgetRequests.Attach(request).BudgetHistories.RemoveAll(h =>h.BudgetRequest.Id.Equals(request.Id));
                        context.BudgetRequests.Attach(request).BudgetDescriptions.RemoveAll(d => d.BudgetRequest.Id.Equals(request.Id));
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

        public BudgetRequest Get(Guid id)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    BudgetRequest request = context.BudgetRequests
                        .AsNoTracking()
                        .FirstOrDefault(e => e.Id.Equals(id));
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
