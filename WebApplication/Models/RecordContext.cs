using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class RecordContext : DbContext
    {
        //The constructor just calls the constructor in the DbContext class
        public RecordContext(DbContextOptions<RecordContext> options) : base(options) { }

        //We only have a single table with Items which is represented by this DbSet
        public DbSet<Record> Records { get; set; }
    }
}