using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CourseRatingDto
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        
        public Guid CategoryId { get; set; }

        public int UserId { get; set; }

        public double Value { get; set; }
    }
}
