using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Services.CourseService
{
    public interface ICourseRepository
    {    
        IEnumerable<Course> GetCourses(Guid categoryId);
        Course GetCourse(Guid categoryId, Guid courseId);
        void AddCourse(Guid categoryId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        bool CourseExists(Guid courseId);
        bool Save();
        bool CategoryExists(Guid categoryId);
    
    }
}
