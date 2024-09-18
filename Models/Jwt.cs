namespace DentalClinic.Models
{
    public class Jwt(string key,string issuer,string audience,double durationInDays)
    {
        public string Key { get; set; } = key;
        public string Issuer { get; set; } = issuer;

        public double DurationInDays { get; set; } = durationInDays;    

        public string Audience { get; set; } = audience;

    }
}
