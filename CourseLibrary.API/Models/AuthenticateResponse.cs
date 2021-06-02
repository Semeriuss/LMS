using System;
namespace CourseLibrary.API.Models
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
