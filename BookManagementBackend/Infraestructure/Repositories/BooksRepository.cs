using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Repositories
{
    public class BooksRepository(LibraryContext db) : IBooksRepository
    {
        private readonly LibraryContext _db = db;

        public async Task AddBook(Books book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteBook(Books book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<Books?> GetBook(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public async Task<Books?> GetBook(string isbn)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Isbn10 == isbn || b.Isbn13 == isbn);
        }

        public async Task<List<Books>> GetAllBooks()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task UpdateBook(Books book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> BookExists(string isbn)
        {
            return await _db.Books.AnyAsync(b => b.Isbn10 == isbn || b.Isbn13 == isbn);
        }
    }
}
