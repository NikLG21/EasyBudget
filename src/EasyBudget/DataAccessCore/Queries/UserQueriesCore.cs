using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;
using Microsoft.EntityFrameworkCore;

namespace DataAccessCore.Queries
{
    public class UserQueriesCore : IUserQueries
    {
        private readonly BudgetRequestDbContextCoreFactory _factory;

        public UserQueriesCore(BudgetRequestDbContextCoreFactory factory)
        {
            _factory = factory;
        }

        public Guid GetUserByLogin(string login, string password)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
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
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
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

        public List<UserMainInfoDto> GetUsers()
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    List<UserMainInfoDto> list = context.Users.AsNoTracking().Select(u => new UserMainInfoDto(u.Id)
                    {
                        Name = u.Name,
                        Login = u.Login,
                        Roles = u.Roles,
                        UnitId = u.Unit.Id,
                        UnitName = u.Unit.Name,
                    }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }

        public UserMainInfoDto GetMainInfo(Guid userId)
        {
            using (BudgetRequestDbContextCore context = _factory.Create())
            {
                try
                {
                    return context.Users.AsNoTracking().Where(u => u.Id == userId).Select(u =>
                        new UserMainInfoDto(u.Id)
                        {
                            Name = u.Name,
                            Login = u.Login,
                            Roles = u.Roles,
                            UnitId = u.Unit.Id,
                            UnitName = u.Unit.Name,
                        }).FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
