using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelCreating_MainConfig(modelBuilder);
            OnModelCreating_Entity(modelBuilder);
        }

        private static void OnModelCreating_MainConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        private static void OnModelCreating_Entity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Where(p => p.Name == "Id").Configure(p => p.IsKey().HasColumnOrder(0));
            modelBuilder.Properties().Where(x => x.PropertyType == typeof(string)).Configure(x => x.IsRequired());
        }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<BudgetRequest> BudgetRequests { get; set; }
    }
}
