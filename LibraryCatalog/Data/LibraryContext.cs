using Microsoft.EntityFrameworkCore;
using LibraryCatalog.Models;

namespace LibraryCatalog.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
    }
}
