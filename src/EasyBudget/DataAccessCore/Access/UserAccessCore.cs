using System;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace DataAccessCore.Access
{
    public class UserAccessCore : IUserAccess
    {
        private readonly IBudgetRequestDbContextCoreFactory _factory;

        public UserAccessCore(IBudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }
        public void Add(User user)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
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
            using (BudgetRequestDbContextCore context = _factory.Create())
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

        public User Get(Guid id)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    User user = context.Users.AsNoTracking()
                        .Include(u => u.Unit)
                        .Include(u => u.Roles)
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
        private static void ProcessDbUpdateException(DbUpdateException e)
        {
            SqlException sqlException = e.InnerException?.InnerException as SqlException;
            if (sqlException != null && sqlException.Number == 2601)
            {
                throw new DuplicateEntryException("Користувач", e);
            }

            throw new CriticalException(e);
        }
    }
}
