using Microsoft.EntityFrameworkCore;
using OutsideInTestingDemo.App.DataLayer;

namespace OutsideInTestingDemo.App.PostgreSql;

public class PostgreDBContext(DbContextOptions<PostgreDBContext> options) : AppDbContext(options)
{
}
