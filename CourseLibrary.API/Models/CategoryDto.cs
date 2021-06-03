using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
