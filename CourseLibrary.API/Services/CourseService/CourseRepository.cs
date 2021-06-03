using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseLibrary.API.Services.CourseService
{
    public class CourseRepository : ICourseRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public CourseRepository(CourseLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public IEnumerable<Course> GetCourses()
        //{
        //    return _context.Courses.ToList<Course>();
        //}

        public IEnumerable<Course> GetCourses(Guid categoryId, CourseParameters courseParameters)
        {
            if (string.IsNullOrWhiteSpace(courseParameters.UserId.ToString()) && string.IsNullOrWhiteSpace(courseParameters.SearchQuery))
            {
                return GetCourses(categoryId);
            }

            var collection = _context.Courses as IQueryable<Course>;

            if (!string.IsNullOrWhiteSpace(courseParameters.UserId.ToString()))
            {
                var id = courseParameters.UserId;
                collection = collection.Where(a => a.UserId == id);
            }

            if (!string.IsNullOrWhiteSpace(courseParameters.SearchQuery))
            {
                var searchQuery = courseParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.ToLower().Contains(searchQuery.ToLower()) || a.Description.ToLower().Contains(searchQuery.ToLower()));
            }

            return collection.ToList();
        }

        public void AddCourse(Guid categoryId, Course course)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            // always set the categoryId to the passed-in categoryId
            course.CategoryId = categoryId;
            _context.Courses.Add(course); 
        }         

        public void DeleteCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            _context.Courses.Remove(course);
        }
  
        public Course GetCourse(Guid categoryId, Guid courseId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses
              .Where(c => c.CategoryId == categoryId && c.Id == courseId).FirstOrDefault();
        }

        public IEnumerable<Course> GetCourses(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            return _context.Courses
                        .Where(c => c.CategoryId == categoryId)
                        .OrderBy(c => c.Title)
                        .ToList();
        }

        public void UpdateCourse(Course course)
        {
            // no code in this implementation
        }

        public bool CourseExists(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses.Any(a => a.Id == courseId);
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

    }
}
