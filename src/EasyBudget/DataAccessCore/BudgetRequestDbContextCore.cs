﻿using System;
using System.Linq;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Action = EasyBudget.Common.Model.Security.Action;

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
            modelBuilder.Entity<User>().ToTable(nameof(User));

            modelBuilder.Entity<User>().Property(u => u.Login).HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(40);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(100);
            modelBuilder.Entity<User>().HasOne(u => u.Unit).WithMany().IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_BudgetRequestConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetRequest>().ToTable(nameof(BudgetRequest));

            modelBuilder.Entity<BudgetRequest>().Property(p => p.Name).HasMaxLength(200);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Requester).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Approver).WithMany().IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Executor).WithMany().IsRequired(false).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Department).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Unit).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_BudgetDescriptionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetDescription>().ToTable(nameof(BudgetDescription));

            modelBuilder.Entity<BudgetDescription>().Property(p => p.Description).HasMaxLength(2000);
            modelBuilder.Entity<BudgetDescription>().HasOne(u => u.User).WithMany().IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void OnModelCreating_RoleConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable(nameof(Role));

            modelBuilder.Entity<Role>().Property(p => p.Name).HasMaxLength(20);
            modelBuilder.Entity<Role>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Role>().HasOne(u => u.Department).WithMany().IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void OnModelCreating_ActionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>().ToTable(nameof(Action));

            modelBuilder.Entity<Action>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Action>().HasIndex(p => p.Name).IsUnique();
        }

        private static void OnModelCreating_DepartmentConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable(nameof(Department));

            modelBuilder.Entity<Department>().Property(p => p.Name).HasMaxLength(30);
            modelBuilder.Entity<Department>().HasIndex(p => p.Name).IsUnique();
        }

        private static void OnModelCreating_BudgetHistoryConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetHistory>().ToTable(nameof(BudgetHistory));

            modelBuilder.Entity<BudgetHistory>().HasOne(b => b.User).WithMany().IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_UnitConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().ToTable(nameof(Unit));

            modelBuilder.Entity<Unit>().Property(p => p.Name).HasMaxLength(30);
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
