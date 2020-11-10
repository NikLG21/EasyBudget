using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using DataAccessCore.Extensions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Action = EasyBudget.Common.Model.Security.Action;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace DataAccessCore
{
    public class BudgetRequestDbContextCore : DbContext
    {
        private string _connectionString;
        public BudgetRequestDbContextCore(string connectionString)
        {
            _connectionString = connectionString;
            this.Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //OnModelCreating_MainConfig(modelBuilder);
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


        //private static void OnModelCreating_MainConfig(ModelBuilder modelBuilder)
        //{
            
        //}

        private static void OnModelCreating_Entity(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t=>t.GetProperties())
                .Where(p=>p.ClrType == typeof(string)))
            {
                property.AsProperty().Builder.IsRequired(true, ConfigurationSource.Convention);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(Guid)))
            {
                property.IsPrimaryKey();
            }
        }

        private static void OnModelCreating_UserConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Login).HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(40);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(100);
            modelBuilder.Entity<User>().HasOne(u => u.Unit).WithMany().IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        }
        private static void OnModelCreating_BudgetRequestConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetRequest>().Property(p => p.Name).HasMaxLength(200);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Requester).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Approver).WithMany().IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Executor).WithMany().IsRequired(false).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Department).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Unit).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }
        private static void OnModelCreating_BudgetDescriptionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetDescription>().Property(p => p.Description).HasMaxLength(2000);
            modelBuilder.Entity<BudgetDescription>().HasOne(u => u.User).WithMany().IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private static void OnModelCreating_RoleConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().Property(p => p.Name).HasMaxLength(20);
            modelBuilder.Entity<Role>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Role>().HasOne(u => u.Department).WithMany().IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private static void OnModelCreating_ActionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Action>().HasIndex(p => p.Name).IsUnique();
        }
        private static void OnModelCreating_DepartmentConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().Property(p => p.Name).HasMaxLength(30);
            modelBuilder.Entity<Department>().HasIndex(p => p.Name).IsUnique();
        }
        private static void OnModelCreating_BudgetHistoryConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetHistory>().HasOne(b => b.User).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }
        private static void OnModelCreating_UnitConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().Property(p => p.Name).HasMaxLength(30);
            modelBuilder.Entity<Unit>().HasIndex(p => p.Name).IsUnique();

        }
        public System.Data.Entity.DbSet<Action> Actions { get; set; }
        public System.Data.Entity.DbSet<Role> Roles { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<BudgetRequest> BudgetRequests { get; set; }
        public System.Data.Entity.DbSet<BudgetDescription> BudgetDescriptions { get; set; }
        public System.Data.Entity.DbSet<Department> Departments { get; set; }
        public System.Data.Entity.DbSet<Unit> Units { get; set; }
        public System.Data.Entity.DbSet<BudgetHistory> BudgetHistories { get; set; }
    }
}
