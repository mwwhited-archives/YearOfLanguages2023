using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HashEm.Persistence;

public class HashDbContext(
        DbContextOptions<HashDbContext> options,
        IOptions<HashingOptions> config
        ) : DbContext(options)
{
    public DbSet<ImageFile> ImageFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(config.Value.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
