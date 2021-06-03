using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Services.ContentService
{
    public interface IContentRepository
    {    
        IEnumerable<Content> GetContents(Guid categoryId, Guid courseId);
        Content GetContent(Guid categoryId, Guid courseId, Guid contentId);
        void AddContent(Guid categoryId, Guid courseId, Content content);
        void UpdateContent(Content content);
        void DeleteContent(Content content);
        bool Save();
        bool CategoryExists(Guid categoryId);
        bool CourseExists(Guid courseId);
    }
}
