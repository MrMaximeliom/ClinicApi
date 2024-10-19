using DentalClinic.Models;

namespace DentalClinic.DataTransferObjects
{
    public record UsersDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string FourthName { get; set; }

        public string UserType { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdatedAt { get; set; }

        // Navigation properties
        public Doctor Doctor { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }


    }
}
