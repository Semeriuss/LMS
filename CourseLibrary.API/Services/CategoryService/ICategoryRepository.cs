using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Services.CategoryService
{
    public interface ICategoryRepository
    {    
        IEnumerable<Category> GetCategories();
        bool CategoryExists(Guid categoryId);
        Category GetCategory(Guid categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool Save();
    }
}
