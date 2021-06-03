using System;
namespace CourseLibrary.API.Models
{
    public class ResourceDto
    {
        string FileName { get; set; }
        string FilePath { get; set; }
        
        public Guid Id { get; set; }
    }
}
