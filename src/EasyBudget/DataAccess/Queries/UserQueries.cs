using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Model.Security;
using Action = EasyBudget.Common.Model.Security.Action;

namespace DataAccess.Queries
{
    public class UserQueries : IUserQueries
    {
        public Guid GetUserByLogin(string login, string password)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                User user = context.Users.AsNoTracking().FirstOrDefault(u => (u.Login == login && u.Password == password));
                if (user != null)
                {
                    return user.Id;
                }

                return Guid.Empty;
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

                return context
                    .Users
                    .AsNoTracking()
                    .Where(u => u.Id == userId)
                    .SelectMany(u => u.Roles)
                    .SelectMany(r => r.Actions).Distinct()
                    .Select(a => a.Name)
                    .ToList();

                ////context.Users.AsNoTracking().Where(u => u.Id == userId).Select(u => u.Roles).SelectMany(r => r.)
                //List<Role> roles = context.Users.FirstOrDefault(u => u.Id == userId).Roles;
                //List<Action> actions = new List<Action>();
                //foreach (Role role in roles)
                //{
                //    actions.AddRange(context.Roles.Find(role).Actions);
                //}
                //List<string> actionsList = new List<string>();
                //foreach (Action action in actions)
                //{
                //    actionsList.Add(action.Name);
                //}

                //return actionsList;
            }
        }
    }
}
