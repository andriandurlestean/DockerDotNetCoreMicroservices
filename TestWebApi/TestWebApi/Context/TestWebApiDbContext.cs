using Microsoft.EntityFrameworkCore;
using TestWebApi.Models;

namespace TestWebApi.Context
{
    public class TestWebApiDbContext : DbContext
    {
        public TestWebApiDbContext(DbContextOptions<TestWebApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }
    }
}