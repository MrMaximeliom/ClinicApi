using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class RequestToken
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
