using BookManagementBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Contexts
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<BooksReturn> BooksReturn { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksReturn>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(b => b.Book)
                    .WithMany()
                    .HasForeignKey(b => b.BookId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(a => a.Returns)
                    .WithOne(b => b.Book)
                    .HasForeignKey(b => b.BookId)
                    .HasPrincipalKey(a => a.Id);
            });
        }
    }
}
