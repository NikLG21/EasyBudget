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
            OnModelCreating_UserConfig(modelBuilder);
            OnModelCreating_BudgetRequestConfig(modelBuilder);
            OnModelCreating_BudgetDescriptionConfig(modelBuilder);
            OnModelCreating_RoleConfig(modelBuilder);
            OnModelCreating_ActionConfig(modelBuilder);
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

        private static void OnModelCreating_UserConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Login).HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(40);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(20);
        }

        private static void OnModelCreating_BudgetRequestConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetRequest>().Property(p => p.Name).HasMaxLength(200);
        }

        private static void OnModelCreating_BudgetDescriptionConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetDescription>().Property(p => p.Description).HasMaxLength(2000);
        }
        private static void OnModelCreating_RoleConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().Property(p => p.Name).HasMaxLength(20);
        }

        private static void OnModelCreating_ActionConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>().Property(p => p.Name).HasMaxLength(50);
        }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BudgetRequest> BudgetRequests { get; set; }
        public DbSet<BudgetDescription> BudgetDescriptions { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
