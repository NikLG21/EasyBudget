using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;

namespace DataAccess.Access
{
    public class UserAccess : IUserAccess
    {
        public void Add(User user)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    SqlException sqlException = e.InnerException?.InnerException as SqlException;
                    if (sqlException != null && sqlException.Number == 2601)
                    {
                        throw new DuplicateEntryException("Пользователь", e);
                    }

                    throw new CriticalException(e);
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public void Update(User user)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    context.Users.Add(user);
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    SqlException sqlException = e.InnerException?.InnerException as SqlException;
                    if (sqlException != null && sqlException.Number == 2601)
                    {
                        throw new DuplicateEntryException("Пользователь", e);
                    }

                    throw new CriticalException(e);
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }

            }
        }

        public User Get(Guid id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    User user = context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        throw new EntityNotFoundException("Пользователь");
                    }
                    return user;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }

            }
        }
    }
}
