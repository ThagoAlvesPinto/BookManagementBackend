using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Repositories
{
    public class BooksRepository(LibraryContext _db) : IBooksRepository
    {
        private readonly LibraryContext db = _db;

        public async Task AddBook(Books book)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBook(Books book)
        {
            db.Books.Remove(book);
            await db.SaveChangesAsync();
        }

        public async Task<Books?> GetBook(int id)
        {
            return await db.Books.FindAsync(id);
        }

        public async Task<Books?> GetBook(string isbn)
        {
            return await db.Books.FirstOrDefaultAsync(b => b.Isbn10 == isbn || b.Isbn13 == isbn);
        }

        public async Task<IEnumerable<Books>> GetAllBooks()
        {
            return await db.Books.ToListAsync();
        }

        public async Task UpdateBook(Books book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
        }
    }
}
