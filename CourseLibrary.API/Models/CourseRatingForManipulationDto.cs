using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public abstract class CourseRatingForManipulationDto
    {
        [Range(1,5, ErrorMessage = "The rating shouldn't be less than one or more than 5 stars.")]
        public double Value { get; set; }
    }
}
