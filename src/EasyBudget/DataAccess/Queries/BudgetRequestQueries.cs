using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Utils;

namespace DataAccess.Queries
{
    public class BudgetRequestQueries : IBudgetRequestQueries
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetRequestQueries(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }

        public List<BudgetRequestMainListDto> GetBudgetRequestsByRequestor(Guid userId, DateTime from, DateTime to)
        {
            //int pageSize = 20;
            //int pageNumber = 3;

            //using (BudgetRequestDbContext context = _factory.Create())
            //{
            //    int total = context
            //        .BudgetRequests
            //        .AsNoTracking()
            //        .Where(br => br.Requester.Id == userId)
            //        .Count();

            //    List<BudgetRequestMainListDto> list = context
            //        .BudgetRequests
            //        .AsNoTracking()
            //        .Where(br => br.Requester.Id == userId)
            //        .OrderByDescending(br => br.DateRequested)
            //        .Skip(pageSize * pageNumber)
            //        .Take(pageSize)
            //        .Select(br => new BudgetRequestMainListDto
            //        {
            //            Name = br.Name,
            //            RequesterName = br.Requester.Name,
            //            DepartmentName = br.Department.Name,
            //            DateRequested = br.DateRequested,
            //            State = br.State,
            //            RealPrice = br.RealPrice
            //        }).ToList();

            //    PagingList<BudgetRequestMainListDto> pagingList = new PagingList<BudgetRequestMainListDto>
            //    {
            //        Data = list,
            //        Total = total,
            //        PageNumber = pageNumber,
            //        PageSize = pageSize
            //    };

            //   return pagingList;
            //}

            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Requester.Id == userId)
                        .Where(br => br.DateRequested >= from && br.DateRequested <= to)
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

        public List<BudgetRequestMainListDto> GetBudgetRequestsByApprover(Guid userId, DateTime from, DateTime to)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Approver.Id == userId)
                        .Where(br => br.DateRequested >= from && br.DateRequested <= to)
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

        public List<BudgetRequestMainListDto> GetBudgetRequestByExecutor(Guid userId, DateTime from, DateTime to)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Executor.Id == userId)
                        .Where(br => br.DateRequested >= from && br.DateRequested <= to)
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

        public List<BudgetRequestMainListDto> GetBudgetRequestByTime(DateTime from, DateTime to)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.DateRequested >= from && br.DateRequested <= to)
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

        public List<BudgetRequestMainListDto> GetBudgetRequestUnapprovedDirectors(BudgetState state)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.State == state)
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
        public List<BudgetRequestMainListDto> GetBudgetRequestUnapprovedApprover(Unit unit)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Unit == unit)
                        .Where(br => br.State == BudgetState.Requested)
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
        public List<BudgetRequestMainListDto> GetBudgetRequestUncheckedExecutor(Department department)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Department == department)
                        .Where(br => br.State== BudgetState.ApprovedFirstLine)
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
        public List<BudgetRequestMainListDto> GetBudgetRequestExecution(Department department)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Department == department)
                        .Where(br => br.State == BudgetState.Execution)
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
        public List<BudgetRequestMainListDto> GetBudgetRequestUnapprovedRequestor(Guid userId)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Requester.Id == userId)
                        .Where(br => br.State == BudgetState.Requested)
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
