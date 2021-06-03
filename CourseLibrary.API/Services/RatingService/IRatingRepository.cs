using CourseLibrary.API.Entities;
using System;

namespace CourseLibrary.API.Services.RatingService
{
    public interface IRatingRepository
    {
        CourseRating GetRating(Guid categoryId, Guid courseId, Guid ratingId);
        void AddRating(Guid categoryId, Guid courseId, CourseRating courseRating);
        void DeleteRating(CourseRating courseRating);
        double GetRatings(Guid courseId);
        void UpdateRating(CourseRating courseRating);
        bool CourseRatingExists(Guid courseId);
        bool Save();
        bool CategoryExists(Guid categoryId);
        bool CourseExists(Guid courseId);

    }
}
