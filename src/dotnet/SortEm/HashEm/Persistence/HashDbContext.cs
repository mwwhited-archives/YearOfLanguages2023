using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashEm.Persistence;

public class HashDbContext : DbContext
{
    public HashDbContext(DbContextOptions<HashDbContext> options) : base(options)
    {
    }

    public DbSet<ImageFile> ImageFiles { get; set; }
}

public class ImageFile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RealativePath { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public string Hash { get; set; } = null!;
    public string PathHash { get; set; } = null!;
}
