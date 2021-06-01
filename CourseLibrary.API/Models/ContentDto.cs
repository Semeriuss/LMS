using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class ContentDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }
    }
}
