using Microsoft.EntityFrameworkCore;

using Project.Domain.Developers;

using Starlight.Core.DbHelper;

namespace Project.Domain.Context
{
    public interface IDataBaseContext : IDbContext
    {
        DbSet<Developer> Developers { get; }
    }
}
