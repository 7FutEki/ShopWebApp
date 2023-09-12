using ShopWebApp.Models;

namespace ShopWebApp.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByIdAsync(int id);
        Task<Admin> GetAdminByIdAsyncNoTracking(int id);
        Task<Admin> GetAdminByLoginAsync(string login);
        Task<IEnumerable<User>> GetAllUserAsync();


        bool Add(Admin admin);
        bool Delete(Admin admin);
        bool Update(Admin admin);
        bool Save();
    }
}
