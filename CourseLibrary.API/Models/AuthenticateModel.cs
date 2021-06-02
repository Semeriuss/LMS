using System;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
