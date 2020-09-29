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

        public void UpdateList(List<BudgetRequestMainListDto> requests)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    //TODO: Please check my version
                    List<BudgetRequest> budgetRequests = context.BudgetRequests
                        .Where(br => requests.Select(r => r.Id).ToList().Contains(br.Id))
                        .ToList();
                    //List<BudgetRequest> budgetRequests1 = context.BudgetRequests
                    //    .Where(br => requests.Any(r => r.Id == br.Id))
                    //    .ToList();

                    //TODO: This is does not work. i is not changed. I think we need function with (List<Guid> requestIds, State newState)
                    int i;
                    foreach (BudgetRequest request in budgetRequests)
                    {
                        i=requests.FindIndex(r => r.Id.Equals(request.Id));
                        request.State = requests[i].State;
                    }

                    context.Entry(budgetRequests).State = EntityState.Modified;
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
                    //TODO: You have to delete BudgedRequest with collection of history and description. Otherwise you you will get exception
                    BudgetRequest request = context.BudgetRequests.FirstOrDefault(e => e.Id == id);
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

        public List<BudgetRequestMainListDto> GetList(List<Guid> ids)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context.BudgetRequests
                        .AsNoTracking()
                        .Where(bd => ids.Contains(bd.Id))
                        .Select(br => new BudgetRequestMainListDto()
                        {
                            Id = br.Id,
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice,
                            UnitName = br.Unit.Name
                        })
                        .ToList();
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
