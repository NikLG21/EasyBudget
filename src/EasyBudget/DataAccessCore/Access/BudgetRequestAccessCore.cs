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
                    //string s1 = DataAccessUtils.Dump(context);

                    context.Departments.AsNoTracking().FirstOrDefault(e => e.Id == request.Department.Id);
                    context.Units.AsNoTracking().FirstOrDefault(e => e.Id == request.Unit.Id);
                    context.Users.AsNoTracking().FirstOrDefault(e => e.Id == request.Requester.Id);

                    //string s2 = DataAccessUtils.Dump(context);

                    context.BudgetRequests.Add(request);

                    context.Entry(request.Requester).State = EntityState.Unchanged;
                    context.Entry(request.Department).State = EntityState.Unchanged;
                    context.Entry(request.Unit).State = EntityState.Unchanged;

                    //string s3 = DataAccessUtils.Dump(context);

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

        public void UpdateList(List<Guid> ids, BudgetState newState)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
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

                    //context.Entry(request).State = EntityState.Detached;
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
