using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{

    [CourseTitleMustBeDifferentFromDescriptionAttribute(
        ErrorMessage = "Title must be different from description")]
    public class CourseForCreationDto // : IValidatableObject
    {

    }
}
