using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace OutsideInTestingDemo.App.DataLayer;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
