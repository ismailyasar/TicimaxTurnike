using Microsoft.EntityFrameworkCore;
using TicimaxTurnike.Entity;

namespace TicimaxTurnike.Data.Concrete;

public class AppDbContext:DbContext
{
    public AppDbContext()
    {
        
    }
    public AppDbContext(DbContextOptions options):base(options)
    {
        
    }
    
    public DbSet<Person> Persons;
    public DbSet<Entry> Entries { get; set; }
    public DbSet<LastEntryDetail> LastEntryDetails { get; set; }
}