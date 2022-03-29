using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models;
using CodeBusters.Models.Category;

namespace CodeBusters.Services.Category
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(CategoryCreate request);
        Task<IEnumerable<CategoryListItem>> GetAllCategoryAsync();
    }
}