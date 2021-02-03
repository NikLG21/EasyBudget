using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace DataAccessCore.Access
{
    public class BudgetRequestAccessCore :IBudgetRequestAccess
    {
        private readonly IBudgetRequestDbContextCoreFactory _factory;

        public BudgetRequestAccessCore(IBudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }
        public void Add(BudgetRequest request)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    context.BudgetRequests.Add(request);

                    context.Entry(request.Requester).State = EntityState.Unchanged;
                    context.Entry(request.Department).State = EntityState.Unchanged;
                    context.Entry(request.Unit).State = EntityState.Unchanged;
                    
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
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    context.BudgetRequests.Attach(request);
                    context.Entry(request).State = EntityState.Modified;
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
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    BudgetRequest request = context.BudgetRequests.FirstOrDefault(e => e.Id == id);
                    if (request != null)
                    {
                        context.BudgetRequests.Attach(request).Entity.BudgetHistories
                            .RemoveAll(h => h.BudgetRequest.Id.Equals(request.Id));
                        context.BudgetRequests.Attach(request).Entity.BudgetDescriptions.RemoveAll(d => d.BudgetRequest.Id.Equals(request.Id));
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
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    BudgetRequest request = context.BudgetRequests
                        .Include(br => br.Approver)
                        .Include(br => br.Requester)
                        .Include(br => br.Executor)
                        .Include(br => br.Unit)
                        .Include(br => br.Department)
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

        public BudgetRequest GetSimpleRequest(Guid id)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
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
