using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.Departments.Add(department);
                context.Entry(department).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                Department department = context.Departments.FirstOrDefault(d => d.Id == id);
                if (department != null)
                {
                    context.Departments.Attach(department);
                    context.Departments.Remove(department);
                    context.SaveChanges();
                }
            }
        }

        public Department Get(Guid id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                return context.Departments.AsNoTracking().FirstOrDefault(d => d.Id == id);
            }
        }
    }
}
