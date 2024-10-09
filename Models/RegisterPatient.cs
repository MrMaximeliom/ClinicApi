namespace DentalClinic.Models
{
    public class RegisterPatient(string firstName,string lastName,string phoneNumber,string email,string password)
    {
        public string FirstName { get; set; } = firstName;

        public string LastName { get; set; } = lastName;


        public string Email {  get; set; }  = email;

        public string PhoneNumber { get; set; } = phoneNumber;

        public string Password { get; set; }    = password;
    }
}
