using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (await GetUserByEmailAsync(model.Email) != null)
                return false;

            var entity = new UserEntity
            {
                FullName = model.FullName,
                Email = model.Email,
            };

            var passwordHasher = new PasswordHasher<UserEntity>();

            entity.Password = passwordHasher.HashPassword(entity, model.Password);

            _context.Users.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<UserDetail> GetUserByIdAsync(int userId)
        {
            var entity = await _context.Users.FindAsync(userId);
            if (entity is null)
            return null;

            var userDetail = new UserDetail
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Email = entity.Email,
                isAdmin = entity.isAdmin
            };

            return userDetail;
        }

        public async Task<List<UserListItem>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Select (entity => new UserListItem
                {
                    Id = entity.Id,
                    FullName = entity.FullName
                })
                .ToListAsync();

            return users;
        }

        public async Task<bool> UpdateUserAsync(UserUpdate model)
        {
            var userEntity = await _context.Users.FindAsync(model.Id);
            if (userEntity is null)
            return false;

            userEntity.FullName = model.FullName;
            userEntity.Email = model.Email;
            userEntity.Password = model.Password;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var userEntity = await _context.Users.FindAsync(userId);
            if (userEntity is null)
            return false;

            _context.Users.Remove(userEntity);
            return await _context.SaveChangesAsync() == 1;
        }
    

        private async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
        }


    }
}