using Microsoft.AspNetCore.Identity;

namespace DentalClinic.Models
{
    public class User(string FirstName,string SecondName,string ThirdName,string FourthName):IdentityUser
    {
        public string FirstName { get; set; } = FirstName; 

        public string SecondName { get; set; } = SecondName ;

        public string ThirdName { get; set; } = ThirdName;

        public string FourthName {get; set; } = FourthName ;   

        public UserType UserType { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now ;

        public DateTime? LastUpdatedAt { get; set; }


        // Navigation properties
        public Doctor Doctor { get; set; }  

        public List<RefreshToken>? RefreshTokens { get; set; }






    }
}
