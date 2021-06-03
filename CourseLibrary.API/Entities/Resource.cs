using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseLibrary.API.Entities
{
    public class Resource
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public Guid ContentId { get; set; }

        public Guid Id { get; set; }
    }
}
