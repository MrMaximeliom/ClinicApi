using Microsoft.AspNetCore.Identity;

namespace DentalClinic.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string FourthName { get; set; }

        public UserType UserType { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdatedAt { get; set; }

        public User(string firstName, string secondName, string thirdName, string fourthName)
        {
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            FourthName = fourthName;


        }
        public User()
        {

        }





        // Navigation properties
        public Doctor Doctor { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }






    }
}
