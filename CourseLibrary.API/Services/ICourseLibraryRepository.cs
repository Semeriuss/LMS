using CourseLibrary.API.Entities;
using CourseLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface ICourseLibraryRepository
    {    
        IEnumerable<Course> GetCourses(Guid authorId);
        Course GetCourse(Guid authorId, Guid courseId);
        void AddCourse(Guid authorId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        IEnumerable<Author> GetAuthors();
        IEnumerable<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        bool CourseExists(Guid courseId);
        bool Save();

        // Content
        IEnumerable<Content> GetContents(Guid authorId, Guid courseId);
        Content GetContent(Guid authorId, Guid courseId, Guid contentId);
        void AddContent(Guid authorId, Guid courseId, Content content);
        //void UpdateContent(Content content);
        //void DeleteContent(Content content);
    }
}
