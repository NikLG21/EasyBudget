using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Model;

namespace DataAccess.Access
{
    public class DepartmentAccess : IDepartmentAccess
    {
        public void Add(Department department)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.Departments.Add(department);
                context.SaveChanges();
            }
        }

        public void Update(Department department)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Department Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
