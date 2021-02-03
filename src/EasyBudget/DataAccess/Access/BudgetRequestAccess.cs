using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using DataAccess.Utils;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Action = EasyBudget.Common.Model.Security.Action;

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
                    string s1 = DataAccessUtils.Dump(context);

                    context.Departments.AsNoTracking().FirstOrDefault(e => e.Id == request.Department.Id);
                    context.Units.AsNoTracking().FirstOrDefault(e => e.Id == request.Unit.Id);
                    context.Users.AsNoTracking().FirstOrDefault(e => e.Id == request.Requester.Id);

                    string s2 = DataAccessUtils.Dump(context);

                    context.BudgetRequests.Add(request);

                    DbEntityEntry<User> requestor = context.Entry(request.Requester);
                    DbEntityEntry<Department> department = context.Entry(request.Department);
                    DbEntityEntry<Unit> unit = context.Entry(request.Unit);
                    
                    requestor.State = EntityState.Unchanged;
                    department.State = EntityState.Unchanged;
                    unit.State = EntityState.Unchanged;
                    

                    string s3 = DataAccessUtils.Dump(context);

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

        public void UpdateList(List<Guid> ids, BudgetState newState, Guid userId)
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
                        .Include(br => br.Approver)
                        .Include(br => br.Requester)
                        .Include(br => br.Executor)
                        .Include(br => br.Unit)
                        .Include(br => br.Department)
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
