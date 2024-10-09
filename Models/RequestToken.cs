using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class RequestToken(string phoneNumber,string password)
    {
        [Required]
        public string PhoneNumber { get; set; } = phoneNumber;
        [Required]
        public string Password { get; set; }    = password;
    }
}
