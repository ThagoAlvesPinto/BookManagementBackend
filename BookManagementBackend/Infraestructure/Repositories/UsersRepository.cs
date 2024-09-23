using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookManagementBackend.Infraestructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LibraryContext _db;
        public UsersRepository(LibraryContext db) 
        { 
            _db = db;
        }

        public async Task<Users?> GetUser(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users?> GetUser(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task AddUser(Users user)
        {
            await _db.Users.AddAsync(user);

            await _db.SaveChangesAsync();
        }

        public async Task UpdateUser(Users user)
        {
            _db.Users.Update(user);

            await _db.SaveChangesAsync();
        }
    }
}
