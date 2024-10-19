using System.Text.Json.Serialization;

namespace DentalClinic.Models
{
    public class RegisterPatient
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string FourthName { get; set; }


        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public UserType UserType { get; set; } = UserType.Patient;
    }
}
