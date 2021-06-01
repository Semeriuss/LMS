using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseLibrary.API.Services
{
    public class CourseLibraryRepository : ICourseLibraryRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public CourseLibraryRepository(CourseLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCourse(Guid authorId, Course course)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            // always set the AuthorId to the passed-in authorId
            course.AuthorId = authorId;
            _context.Courses.Add(course); 
        }         

        public void DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
        }
  
        public Course GetCourse(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses
              .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefault();
        }

        public IEnumerable<Course> GetCourses(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Courses
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title)
                        .ToList();
        }

        public void UpdateCourse(Course course)
        {
            // no code in this implementation
        }

        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            // the repository fills the id (instead of using identity columns)
            author.Id = Guid.NewGuid();

            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            _context.Authors.Add(author);
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.Any(a => a.Id == authorId);
        }

        public bool CourseExists(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses.Any(a => a.Id == courseId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Remove(author);
        }
        
        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.ToList<Author>();
        }

        public IEnumerable<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        {
            if(string.IsNullOrWhiteSpace(authorsResourceParameters.MainCategory)
                && string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
            {
                return GetAuthors();
            }

            var collection = _context.Authors as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.MainCategory))
            {
                var mainCategory = authorsResourceParameters.MainCategory.Trim();
                collection = collection.Where(a => a.MainCategory == mainCategory);
            }

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
            {
                var searchQuery = authorsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.MainCategory.Contains(searchQuery)
                || a.FirstName.Contains(searchQuery) || a.LastName.Contains(searchQuery));
            }

            return collection.ToList();
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
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

        // Content
        public IEnumerable<Content> GetContents(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Content
                .Where(c => c.AuthorId == authorId && c.CourseId == courseId)
                .OrderBy(c => c.Title)
                .ToList();
        }

        public Content GetContent(Guid authorId, Guid courseId, Guid contentId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
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
                .Where(c => c.AuthorId == authorId && c.CourseId == courseId && c.Id == contentId).FirstOrDefault();
        }

        public void AddContent(Guid authorId, Guid courseId, Content content)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            content.AuthorId = authorId;
            content.CourseId = courseId;

            content.Id = Guid.NewGuid();

            _context.Content.Add(content);
        }

        public void UpdateContent(Content content)
        {

        }

        public void DeleteContent(Content content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            _context.Content.Remove(content);
        }

        // RATING

        public CourseRating GetRating(Guid authorId, Guid courseId, Guid ratingId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.CourseRatings
              .Where(r => r.AuthorId == authorId && r.CourseId == courseId && r.Id == ratingId).FirstOrDefault();
        }

        public bool CourseRatingExists(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.CourseRatings.Any(a => a.CourseId == courseId);
        }
            public void AddRating(Guid authorId, Guid courseId, CourseRating courseRating)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseRating == null)
            {
                throw new ArgumentNullException(nameof(courseRating));
            }
            // always set the AuthorId to the passed-in authorId
            courseRating.Id = Guid.NewGuid();
            courseRating.AuthorId = authorId;
            courseRating.CourseId = courseId;
            _context.CourseRatings.Add(courseRating);
        }

        public void DeleteRating(CourseRating courseRating)
        {
            _context.CourseRatings.Remove(courseRating);
        }

        public double GetRatings(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var ratings = _context.CourseRatings
                        .Where(r => r.CourseId == courseId);

            double result = 0;
            //foreach(var rating in ratings)
            //{
            //    result += rating.Value;
            //}

            result = (from r in ratings select r.Value).Average();

            return result;
        }

        public void UpdateRating(CourseRating courseRating)
        {
            // No implementation
        }
    }
}
