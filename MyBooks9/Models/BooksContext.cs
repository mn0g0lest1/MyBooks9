using Microsoft.EntityFrameworkCore;

namespace MyBooks9.Models
{
    public class BooksContext : DbContext
    {
        public DbSet<MyBooksModel> Books { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}