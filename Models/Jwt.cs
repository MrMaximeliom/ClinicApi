namespace DentalClinic.Models
{
    public class Jwt
    {
   
        public string Key { get; set; } 
        public string Issuer { get; set; } 

        public double DurationInDays { get; set; }   

        public string Audience { get; set; }
        public Jwt(string key, string issuer, string audience, double durationInDays)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
            DurationInDays = durationInDays;
        }

        // Empty constructor
        public Jwt()
        {
        }

    }
}
