using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Model.Security;

namespace DataAccess.Access
{
    public class UserAccess : IUserAccess
    {
        public void Add(User user)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.Users.Add(user);
                context.Entry(user).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(User user)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.Users.Add(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public User Get(Guid id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                return context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            }
        }
    }
}
