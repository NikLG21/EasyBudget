using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;

namespace DataAccess.Access
{
    public class UserAccess : IUserAccess
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public UserAccess(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Add(User user)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    context.Users.Add(user);
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

        public void Update(User user)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    context.Users.Attach(user);
                    context.Entry(user).State = EntityState.Modified;
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

        private static void ProcessDbUpdateException(DbUpdateException e)
        {
            SqlException sqlException = e.InnerException?.InnerException as SqlException;
            if (sqlException != null && sqlException.Number == 2601)
            {
                throw new DuplicateEntryException("Користувач", e);
            }

            throw new CriticalException(e);
        }

        public User Get(Guid id)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    User user = context.Users.AsNoTracking()
                        .Include(u=>u.Unit)
                        .Include(u=>u.Roles)
                        .FirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        throw new EntityNotFoundException("Користувач");
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
