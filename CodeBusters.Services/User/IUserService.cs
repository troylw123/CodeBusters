using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Users;

namespace CodeBusters.Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model);
        Task<UserDetail> GetUserByIdAsync(int userId);
        Task<List<UserListItem>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAsync(UserUpdate model);
        Task<bool> ChangeAdminStatusAsync(int userId);
    }
}