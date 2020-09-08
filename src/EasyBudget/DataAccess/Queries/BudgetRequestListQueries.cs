using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace DataAccess.Queries
{
    public class BudgetRequestListQueries : IBudgetRequestListQueries
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetRequestListQueries(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }
        public List<BudgetRequestMainListDto> GetAllRequestDirector(DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.DateRequested >= from )
                        .Select(br => new BudgetRequestMainListDto
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
        public List<BudgetRequestMainListDto> GetAllRequestRequester(Guid userId, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br =>br.Requester.Id == userId)
                        .Where(br => br.DateRequested >= from)
                        .Select(br => new BudgetRequestMainListDto
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public List<BudgetRequestMainListDto> GetAllRequestApprover(Guid unitId, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Unit.Id == unitId)
                        .Where(br => br.DateRequested >= from)
                        .Select(br => new BudgetRequestMainListDto
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }

        }
        public List<BudgetRequestMainListDto> GetAllRequestExecutor(Guid departmentId, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Department.Id == departmentId)
                        .Where(br => br.DateRequested >= from)
                        .Select(br => new BudgetRequestMainListDto
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice
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
