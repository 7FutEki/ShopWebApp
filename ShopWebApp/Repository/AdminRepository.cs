using Microsoft.EntityFrameworkCore;
using ShopWebApp.Data;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;

namespace ShopWebApp.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _db;
        public AdminRepository(ApplicationContext db)
        {
            _db = db;
        }
        public bool Add(Admin admin)
        {
            _db.Add(admin);
            return Save();
        }

        public bool Delete(Admin admin)
        {
            _db.Remove(admin);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await _db.Admins.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Admin> GetAdminByIdAsyncNoTracking(int id)
        {
            return await _db.Admins.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Admin> GetAdminByLoginAsync(string login)
        {
            return await _db.Admins.FirstOrDefaultAsync(log => log.Login == login);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Admin admin)
        {
            _db.Update(admin);
            return Save();
        }
    }
}
