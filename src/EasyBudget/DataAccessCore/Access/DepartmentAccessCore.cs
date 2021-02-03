using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace DataAccessCore.Access
{
    public class DepartmentAccessCore : IDepartmentAccess
    {
        private readonly IBudgetRequestDbContextCoreFactory _factory;

        public DepartmentAccessCore(IBudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }

        public void Add(Department department)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    ProcessDbUpdateException(e);
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public void Update(Department department)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    context.Departments.Attach(department);
                    context.Entry(department).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    ProcessDbUpdateException(e);
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public Department Get(Guid id)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    Department department = context.Departments.AsNoTracking().FirstOrDefault(d => d.Id == id);
                    if (department == null)
                    {
                        throw new EntityNotFoundException("Відділ");
                    }
                    return department;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        private static void ProcessDbUpdateException(DbUpdateException e)
        {
            SqlException sqlException = e.InnerException?.InnerException as SqlException;
            if (sqlException != null && sqlException.Number == 2601)
            {
                throw new DuplicateEntryException("Відділ", e);
            }

            throw new CriticalException(e);
        }
    }
}
