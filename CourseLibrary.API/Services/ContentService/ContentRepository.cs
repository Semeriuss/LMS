using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseLibrary.API.Services.ContentService
{
    public class ContentRepository : IContentRepository
    {
        private readonly CourseLibraryContext _context;

        public ContentRepository(CourseLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Content> GetContents(Guid categoryId, Guid courseId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Content
                .Where(c => c.CategoryId == categoryId && c.CourseId == courseId)
                .OrderBy(c => c.Title)
                .ToList();
        }

        public Content GetContent(Guid categoryId, Guid courseId, Guid contentId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            if (contentId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(contentId));
            }

            return _context.Content
                .Where(c => c.CategoryId == categoryId && c.CourseId == courseId && c.Id == contentId).FirstOrDefault();
        }

        public void AddContent(Guid categoryId, Guid courseId, Content content)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            content.CategoryId = categoryId;
            content.CourseId = courseId;

            content.Id = Guid.NewGuid();

            _context.Content.Add(content);
        }

        public void UpdateContent(Content content)
        {
            // no code in this implementation
        }

        public void DeleteContent(Content content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            _context.Content.Remove(content);
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

        public bool CategoryExists(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            return _context.Categories.Any(a => a.Id == categoryId);
        }
        public bool CourseExists(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses.Any(a => a.Id == courseId);
        }

    }
}
