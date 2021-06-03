using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseLibrary.API.Services.CategoryService
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CourseLibraryContext _context;

        public CategoryRepository(CourseLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList<Category>();
        }
        
        public Category GetCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            return _context.Categories
                .Where(c => c.Id == categoryId).FirstOrDefault();
        }

        public IEnumerable<Category> GetCategories(CategoryParameters categoryParameters)
        {
            if (string.IsNullOrWhiteSpace(categoryParameters.Title) && string.IsNullOrWhiteSpace(categoryParameters.SearchQuery))
            {
                return GetCategories();
            }

            var collection = _context.Categories as IQueryable<Category>;

            if (!string.IsNullOrWhiteSpace(categoryParameters.Title))
            {
                var title = categoryParameters.Title.Trim().ToLower();
                collection = collection.Where(a => a.Title.ToLower() == title);
            }

            if (!string.IsNullOrWhiteSpace(categoryParameters.SearchQuery))
            {
                var searchQuery = categoryParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.ToLower().Contains(searchQuery.ToLower()) || a.Description.ToLower().Contains(searchQuery.ToLower()));
            }

            return collection.ToList();
        }

        public void AddCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            category.Id = Guid.NewGuid();
            _context.Categories.Add(category);
        }
        public void UpdateCategory(Category category)
        {
            // no code in this implementation
        }
        public void DeleteCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Remove(category);
        }

        public bool CategoryExists(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            return _context.Categories.Any(a => a.Id == categoryId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               // dispose resources when needed
            }
        }

    }
}
