using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class BadgeDto
    {
        public Guid VideoBadgePoints { get; set; }


        public Guid NoteBadgePoints { get; set; }


        public Guid QuizBadgePoints { get; set; }
    }
}
