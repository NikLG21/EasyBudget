using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Microsoft.EntityFrameworkCore;
using Action = EasyBudget.Common.Model.Security.Action;

namespace DataAccessCore
{
    public class BudgetRequestDbContextCore : DbContext
    {
        private readonly string _connectionString;

        public BudgetRequestDbContextCore(string connectionString)
        {
            _connectionString = connectionString;
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
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
        
        private static void OnModelCreating_UserConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<User>().Property(u => u.Login).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().HasOne(u => u.Unit).WithMany().HasForeignKey(u => u.UnitId).IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_BudgetRequestConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetRequest>().ToTable(nameof(BudgetRequest));
            modelBuilder.Entity<BudgetRequest>().HasKey(br => br.Id);

            modelBuilder.Entity<BudgetRequest>().Property(p => p.Name).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Requester).WithMany().HasForeignKey(b => b.RequesterId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Approver).WithMany().HasForeignKey(b => b.ApproverId).IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Executor).WithMany().HasForeignKey(b => b.ExecutorId).IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Department).WithMany().HasForeignKey(b=>b.DepartmentId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetRequest>().HasOne(u => u.Unit).WithMany().HasForeignKey(b=>b.UnitId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetRequest>().HasMany(b => b.BudgetHistories).WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BudgetRequest>().HasMany(b => b.BudgetDescriptions).WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void OnModelCreating_BudgetDescriptionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetDescription>().ToTable(nameof(BudgetDescription));
            modelBuilder.Entity<BudgetDescription>().HasKey(bd => bd.Id);

            modelBuilder.Entity<BudgetDescription>().Property(bd => bd.Description).IsRequired().HasMaxLength(2000);
            modelBuilder.Entity<BudgetDescription>().HasOne(bd => bd.User).WithMany().HasForeignKey(bd => bd.UserId)
                .IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetDescription>().HasOne(bd => bd.BudgetRequest).WithMany()
                .HasForeignKey(bd => bd.BudgetRequestId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_RoleConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable(nameof(Role));
            modelBuilder.Entity<Role>().HasKey(r => r.Id);

            modelBuilder.Entity<Role>().Property(p => p.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Role>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Role>().HasOne(u => u.Department).WithMany().HasForeignKey(u => u.DepartmentId)
                .IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_ActionConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>().ToTable(nameof(Action));
            modelBuilder.Entity<Action>().HasKey(p => p.Id);

            modelBuilder.Entity<Action>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Action>().HasIndex(p => p.Name).IsUnique();
        }

        private static void OnModelCreating_DepartmentConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable(nameof(Department));
            modelBuilder.Entity<Department>().HasKey(p => p.Id);

            modelBuilder.Entity<Department>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Department>().HasIndex(p => p.Name).IsUnique();
        }

        private static void OnModelCreating_BudgetHistoryConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetHistory>().ToTable(nameof(BudgetHistory));
            modelBuilder.Entity<BudgetHistory>().HasKey(bh => bh.Id);

            modelBuilder.Entity<BudgetHistory>().HasOne(bh => bh.User).WithMany().HasForeignKey(bh => bh.UserId)
                .IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetHistory>().HasOne(bh => bh.BudgetRequest).WithMany()
                .HasForeignKey(bh => bh.BudgetRequestId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }

        private static void OnModelCreating_UnitConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().ToTable(nameof(Unit));
            modelBuilder.Entity<Unit>().HasKey(p => p.Id);

            modelBuilder.Entity<Unit>().Property(p => p.Name).IsRequired().HasMaxLength(30);
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
