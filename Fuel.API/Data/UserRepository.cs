using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuel.API.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.Include(p => p.ClientProfile).FirstOrDefaultAsync(u => u.Username ==username);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.ClientProfile).ToListAsync();
            return users;
        }

        // Return true if more than > 0 changes else return false
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}