using Microsoft.EntityFrameworkCore;
using OutsideInTestingDemo.App.DataLayer;

namespace OutsideInTestingDemo.App.SqlServer;

public class SqlServerDBContext(DbContextOptions<SqlServerDBContext> options) : AppDbContext(options)
{
}
