﻿namespace DentalClinic.Models
{
    public class Auth
    {

        public string Message { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? UserId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? FourthName { get; set; }
        public List<string>? Roles { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }

        public Auth(string message)
        {
            Message = message;
        }
        public Auth() { }
    }
}
