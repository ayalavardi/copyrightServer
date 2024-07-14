using Dto.models;
using System.Threading.Tasks;

namespace Bl.Interfaces
{
    public interface IUser : IBlcrud<User>
    {
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteByEmailAsync(string email);
        Task<User> LogInAsync(string email, string password);
        Task<User> LogInGoogleAsync(string email, string name);

        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<bool> UpdatePassword(string email, string password);
        bool CheckCorrect(string item);
        string RandomaPassword();
        Task<List<RoleCode>> ReadAllRoleAsync();
        public Task<bool> SendResetEmail(string item, string email);
    }
}
