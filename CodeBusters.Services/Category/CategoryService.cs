using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBusters.Models;
using CodeBusters.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CodeBusters.Models.Category;
using CodeBusters.Data.Entities;
using System;

namespace CodeBusters.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) 
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
        if (!validId)
            throw new Exception("Attempted to build CategoryService without User Id claim.");

            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CategoryListItem>> GetAllCategoryAsync()
        {
            var category = await _dbContext.Categories
                .Where(entity => entity.Id == _userId)
                .Select(entity => new CategoryListItem
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Difficulty = entity.Difficulty
                })
                .ToListAsync();

            return category;
        }
        public async Task<bool> CreateCategoryAsync(CategoryCreate request)
        {
            var categoryEntity = new CategoryEntity
            {
                Name = request.Name,
                Difficulty = request.Difficulty
            };

            _dbContext.Categories.Add(categoryEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<bool> UpdateCategoryAsync(CategoryUpdate request)
        {
            var categoryEntity = await _dbContext.Categories.FindAsync(request.Id);
            if(categoryEntity?.Id != _userId)
                return false;
            
            categoryEntity.Id = request.Id;
            categoryEntity.Name = request.Name;
            categoryEntity.Difficulty = request.Difficulty;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}