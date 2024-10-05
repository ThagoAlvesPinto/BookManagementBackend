using BookManagementBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Contexts
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            #if RELEASE
            Database.Migrate();
            #endif
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
                    .HasForeignKey(b => b.BookId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.ReturnUser)
                    .WithMany()
                    .HasForeignKey(b => b.ReturnUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasMany(a => a.BooksReturn)
                    .WithOne(b => b.ReturnUser)
                    .HasForeignKey(b => b.ReturnUserId)
                    .HasPrincipalKey(a => a.Id);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Isbn10);

                entity.HasIndex(e => e.Isbn13);

                entity.HasMany(a => a.Returns)
                    .WithOne(b => b.Book)
                    .HasForeignKey(b => b.BookId)
                    .HasPrincipalKey(a => a.Id);
            });
        }
    }
}
