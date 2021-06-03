using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;
using CourseLibrary.API.ResourceParameters;

namespace CourseLibrary.API.Services.CategoryService
{
    public interface ICategoryRepository
    {    
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategories(CategoryParameters categoryParameters);
        bool CategoryExists(Guid categoryId);
        Category GetCategory(Guid categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool Save();
    }
}
