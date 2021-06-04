using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Entities
{
    public class Badge
    {
        [Key]
        public Guid BadgeId { get; set; }

        [Required]
        
        public Guid VideoBadgePoints { get; set; }

        
        public Guid NoteBadgePoints { get; set; }

        
        public Guid QuizBadgePoints { get; set; }

        public Guid BadgeAmount { get; set; }

        public Guid UserId { get; set; }
    }
}
