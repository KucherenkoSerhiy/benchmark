using Application;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class EntryContext : DbContext
{
    public DbSet<Entry> Entries { get; set; }
    
    public EntryContext(DbContextOptions<EntryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
    }
}