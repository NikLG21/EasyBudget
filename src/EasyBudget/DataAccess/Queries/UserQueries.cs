using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;

namespace DataAccess.Queries
{
    public class UserQueries : IUserQueries
    {
        public Guid GetUserByLogin(string login, string password)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    User user = context.Users.AsNoTracking().FirstOrDefault(u => (u.Login == login && u.Password == password));
                    if (user != null)
                    {
                        return user.Id;
                    }

                    return Guid.Empty;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }

            }
        }

        public List<string> GetUserActions(Guid userId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                
                //return context
                //    .Roles
                //    .AsNoTracking()
                //    .Where(r => r.Users.Any(u => u.Id == userId))
                //    .SelectMany(r => r.Actions)
                //    .Distinct()
                //    .Select(a => a.Name).ToList();
                try
                {
                    return context
                        .Users
                        .AsNoTracking()
                        .Where(u => u.Id == userId)
                        .SelectMany(u => u.Roles)
                        .SelectMany(r => r.Actions).Distinct()
                        .Select(a => a.Name)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public List<UserMainListDto> GetUsers()
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    List<UserMainListDto> list =  context.Users.AsNoTracking().Select(u => new UserMainListDto
                    {
                        
                        Login= u.Login,
                        Roles = u.Roles
                    }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public UserMainListDto GetMainInfo(Guid id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                try
                {
                    return context.Users.AsNoTracking().Where(u => u.Id == id).Select(u =>
                        new UserMainListDto
                        {
                            Login = u.Login,
                            Roles = u.Roles
                        }).FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
    }
}
