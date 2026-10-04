using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace kitap_deneme_1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
