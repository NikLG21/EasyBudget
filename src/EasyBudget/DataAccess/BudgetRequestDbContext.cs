using System.Data.Entity;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Action = EasyBudget.Common.Model.Security.Action;

namespace DataAccess
{
    public class BudgetRequestDbContext : DbContext
    {
        public BudgetRequestDbContext() : base("name=BudgetRequestDbContext")
        {
        }


        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<BudgetRequest> BudgetRequests { get; set; }
    }
}
