using System;
using System.Linq;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Microsoft.EntityFrameworkCore;
using Action = EasyBudget.Common.Model.Security.Action;

namespace DataAccessCore
{
    public class BudgetRequestDbContextCore : DbContext
    {
        private string _connectionString;
        public BudgetRequestDbContextCore(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            OnModelCreating_Entity(modelBuilder);
            OnModelCreating_UserConfig(modelBuilder);
            OnModelCreating_BudgetRequestConfig(modelBuilder);
            OnModelCreating_BudgetDescriptionConfig(modelBuilder);
            OnModelCreating_RoleConfig(modelBuilder);
            OnModelCreating_ActionConfig(modelBuilder);
            OnModelCreating_DepartmentConfig(modelBuilder);
            OnModelCreating_BudgetHistoryConfig(modelBuilder);
            OnModelCreating_UnitConfig(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionString);
        }
        
        private static void OnModelCreating_Entity(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(Guid)))
            {
                property.IsPrimaryKey();
            }
        }

        private static void OnModelCreating_UserConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));

            modelBuilder.Entity<User>().Property(u => u.Login).IsRequired(true).HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique(true);
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired(true).HasMaxLength(40);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired(true).HasMaxLength(100);
            modelBuilder.Entity<User>().HasOne(u => u.Unit).WithMany().IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_BudgetRequestConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetRequest>().ToTable(nameof(BudgetRequest));

            modelBuilder.Entity<BudgetRequest>().Property(p => p.Name).IsRequired(true).HasMaxLength(200);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Requester).WithMany().HasForeignKey(b => b.RequesterId).IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Approver).WithMany().HasForeignKey(b => b.ApproverId).IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Executor).WithMany().HasForeignKey(b => b.ExecutorId).IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Department).WithMany().HasForeignKey(b=>b.DepartmentId).IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Unit).WithMany().HasForeignKey(b=>b.UnitId).IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetRequest>().HasMany(b => b.BudgetHistories).WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BudgetRequest>().HasMany(b => b.BudgetDescriptions).WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void OnModelCreating_BudgetDescriptionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetDescription>().ToTable(nameof(BudgetDescription));

            modelBuilder.Entity<BudgetDescription>().Property(p => p.Description).IsRequired(true).HasMaxLength(2000);
            modelBuilder.Entity<BudgetDescription>().HasOne(u => u.User).WithMany().IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_RoleConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable(nameof(Role));

            modelBuilder.Entity<Role>().Property(p => p.Name).IsRequired(true).HasMaxLength(20);
            modelBuilder.Entity<Role>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Role>().HasOne(u => u.Department).WithMany().IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_ActionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>().ToTable(nameof(Action));

            modelBuilder.Entity<Action>().Property(p => p.Name).IsRequired(true).HasMaxLength(50);
            modelBuilder.Entity<Action>().HasIndex(p => p.Name).IsUnique();
        }

        private static void OnModelCreating_DepartmentConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable(nameof(Department));

            modelBuilder.Entity<Department>().Property(p => p.Name).IsRequired(true).HasMaxLength(30);
            modelBuilder.Entity<Department>().HasIndex(p => p.Name).IsUnique();
        }

        private static void OnModelCreating_BudgetHistoryConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetHistory>().ToTable(nameof(BudgetHistory));

            modelBuilder.Entity<BudgetHistory>().HasOne(b => b.User).WithMany().IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_UnitConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().ToTable(nameof(Unit));

            modelBuilder.Entity<Unit>().Property(p => p.Name).IsRequired(true).HasMaxLength(30);
            modelBuilder.Entity<Unit>().HasIndex(p => p.Name).IsUnique();
        }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BudgetRequest> BudgetRequests { get; set; }
        public DbSet<BudgetDescription> BudgetDescriptions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<BudgetHistory> BudgetHistories { get; set; }
    }
}
