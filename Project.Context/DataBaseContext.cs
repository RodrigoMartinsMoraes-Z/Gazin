using Microsoft.EntityFrameworkCore;

using Project.Context.TypesConfiguration;
using Project.Domain.Context;
using Project.Domain.Developers;

namespace Project.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DeveloperTypeConfiguration());
        }
    }
}
