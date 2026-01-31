using Microsoft.EntityFrameworkCore;

namespace LibraryBookStatus
{
    public class BookDb : DbContext
    {
        public BookDb(DbContextOptions<BookDb> options) : base(options) { }
        public DbSet<BookStatus> statuses { get; set; }
    }
}
