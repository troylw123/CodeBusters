using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models;

namespace CodeBusters.Services.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListItem>> GetAllCategoryAsync();
    }
}