using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Entities
{
    public class Content
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(50)]
        public string Data { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public Guid CourseId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
