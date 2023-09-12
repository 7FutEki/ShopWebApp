using ShopWebApp.Models;

namespace ShopWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByIdAsyncNoTracking(int id);
        Task<User> GetUserByLoginAsync(string login);

        bool Add(User user);
        bool Delete(User user);
        bool Update(User user);
        bool Save();
    }
}
