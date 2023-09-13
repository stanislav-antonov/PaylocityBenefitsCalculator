using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public class ApiDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dependent> Dependents { get; set; }
    public DbSet<Paycheck> Paychecks { get; set; }
    public DbSet<PaycheckProfile> PaycheckProfiles { get; set; }

    public ApiDbContext(DbContextOptions options) :base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Dependents)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeId)
            .IsRequired(false);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Paychecks)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeId)
            .IsRequired(false);
    }
}
