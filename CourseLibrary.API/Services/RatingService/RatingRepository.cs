using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using System;
using System.Linq;

namespace CourseLibrary.API.Services.RatingService
{
    public class RatingRepository : IRatingRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public RatingRepository(CourseLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CourseRating GetRating(Guid categoryId, Guid courseId, Guid ratingId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.CourseRatings
              .Where(r => r.CategoryId == categoryId && r.CourseId == courseId && r.Id == ratingId).FirstOrDefault();
        }

        public bool CourseRatingExists(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.CourseRatings.Any(a => a.CourseId == courseId);
        }
            public void AddRating(Guid categoryId, Guid courseId, CourseRating courseRating)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (courseRating == null)
            {
                throw new ArgumentNullException(nameof(courseRating));
            }
            // always set the categoryId to the passed-in categoryId
            courseRating.Id = Guid.NewGuid();
            courseRating.CategoryId = categoryId;
            courseRating.CourseId = courseId;
            _context.CourseRatings.Add(courseRating);
        }

        public void DeleteRating(CourseRating courseRating)
        {   
            if (courseRating == null)
            {
                throw new ArgumentNullException(nameof(courseRating));
            }
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
