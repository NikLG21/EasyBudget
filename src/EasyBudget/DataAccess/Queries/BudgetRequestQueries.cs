using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;

namespace DataAccess.Queries
{
    public class BudgetRequestQueries : IBudgetRequestQueries
    {
        public List<BudgetRequestMainListDto> GetBudgetRequestsByOriginator(Guid userId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                List< BudgetRequestMainListDto> list = context
                    .BudgetRequests
                    .AsNoTracking()
                    .Where(br => br.Requester.Id == userId)
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
        }

        public List<BudgetRequestMainListDto> GetBudgetRequestsByApprover(Guid userId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                List<BudgetRequestMainListDto> list = context
                    .BudgetRequests
                    .AsNoTracking()
                    .Where(br => br.Approver.Id == userId)
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
        }

        public List<BudgetRequestMainListDto> GetBudgetRequestByExecutor(Guid userId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                List<BudgetRequestMainListDto> list = context
                    .BudgetRequests
                    .AsNoTracking()
                    .Where(br => br.Executor.Id == userId)
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
        }

        public List<BudgetRequestMainListDto> GetBudgetRequestByTime(DateTime @from, DateTime to)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                List<BudgetRequestMainListDto> list = context
                    .BudgetRequests
                    .AsNoTracking()
                    .Where(br => br.DateRequested >= from && br.DateRequested <=to)
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
        }
    }
}
