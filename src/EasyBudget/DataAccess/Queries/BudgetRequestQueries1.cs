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
    public class BudgetRequestQueries1 : IBudgetRequestQueries1
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetRequestQueries1(IBudgetRequestDbContextFactory factory)
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

        public List<BudgetRequestMainListDto> GetAllRequestApprover(Unit unit, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Unit == unit)
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
        public List<BudgetRequestMainListDto> GetAllRequestExecutor(Role role, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Department == role.Department)
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
