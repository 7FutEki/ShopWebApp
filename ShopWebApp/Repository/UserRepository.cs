using Microsoft.EntityFrameworkCore;
using ShopWebApp.Data;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;

namespace ShopWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }
        public bool Add(User user)
        {
            _db.Add(user);
            return Save();
        }

        public bool Delete(User user)
        {
            _db.Remove(user);
            return Save();
        }

        public async Task<User> GetUserByIdAsyncNoTracking(int id)
        {
            return await _db.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _db.Update(user);
            return Save();
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _db.Users.FirstOrDefaultAsync(log=>log.Login == login);
        }

        
    }
}
