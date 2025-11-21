// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using AutoShopManager.Models;
using Microsoft.EntityFrameworkCore.Sqlite; 

namespace AutoShopManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // ВИКОРИСТАННЯ SQLite: база даних буде у файлі AutoShop.db
                optionsBuilder.UseSqlite("Data Source=AutoShop.db");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Обмеження унікальності VIN
            modelBuilder.Entity<Car>().HasIndex(c => c.VIN).IsUnique();

            // Зв'язок 1:1 Employee <--> User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee).WithOne(e => e.User).HasForeignKey<User>(u => u.EmployeeID).IsRequired();

            // Зв'язки 1:Багато (Sale) - OnDelete(DeleteBehavior.Restrict) забороняє видалення батьківського об'єкта, якщо є дочірні записи
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Car).WithMany(c => c.Sales).HasForeignKey(s => s.CarID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client).WithMany(c => c.Purchases).HasForeignKey(s => s.ClientID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Employee).WithMany(e => e.Sales).HasForeignKey(s => s.EmployeeID).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}