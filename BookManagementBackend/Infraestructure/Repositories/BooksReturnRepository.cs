using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Repositories
{
    public class BooksReturnRepository(LibraryContext db) : IBooksReturnRepository
    {
        private readonly LibraryContext _db = db;

        public async Task AddBookReturn(BooksReturn bookReturn)
        {
            _db.BooksReturn.Add(bookReturn);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteBookReturn(BooksReturn bookReturn)
        {
            _db.BooksReturn.Remove(bookReturn);
            await _db.SaveChangesAsync();
        }

        public async Task<BooksReturn?> GetBookReturn(int id)
        {
            return await _db.BooksReturn.FindAsync(id);
        }

        public async Task<List<BooksReturn>> GetAllBooksReturnByBook(int bookId)
        {
            return await _db.BooksReturn
                .Where(x => x.BookId == bookId)
                .Include(x => x.Book)
                .ToListAsync();
        }
    }
}
