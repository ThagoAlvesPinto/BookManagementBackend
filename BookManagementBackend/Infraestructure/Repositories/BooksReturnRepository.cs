using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Repositories
{
    public class BooksReturnRepository(LibraryContext _db) : IBooksReturnRepository
    {
        private readonly LibraryContext db = _db;

        public async Task AddBookReturn(BooksReturn bookReturn)
        {
            db.BooksReturn.Add(bookReturn);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBookReturn(BooksReturn bookReturn)
        {
            db.BooksReturn.Remove(bookReturn);
            await db.SaveChangesAsync();
        }

        public async Task<BooksReturn?> GetBookReturn(int id)
        {
            return await db.BooksReturn.FindAsync(id);
        }

        public async Task<IEnumerable<BooksReturn>> GetAllBooksReturnByBook(int bookId)
        {
            return await db.BooksReturn
                .Where(x => x.BookId == bookId)
                .Include(x => x.Book)
                .ToListAsync();
        }
    }
}
