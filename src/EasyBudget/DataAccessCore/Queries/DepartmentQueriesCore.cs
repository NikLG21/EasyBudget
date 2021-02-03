using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessCore.Queries
{
    public class DepartmentQueriesCore: IDepartmentQueries
    {
        private readonly IBudgetRequestDbContextCoreFactory _factory;

        public DepartmentQueriesCore(IBudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }
        public List<Department> GetAll()
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    return context.Departments.AsNoTracking().ToList();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
