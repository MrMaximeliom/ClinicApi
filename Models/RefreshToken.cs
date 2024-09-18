namespace DentalClinic.Models
{
    public class RefreshToken(string token,DateTime expiresOn)
    {
        public string Token { get; set; } = token;

        public DateTime ExpiresOn { get; set; } = expiresOn;

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  

        public DateTime? RevokedOn { get; set; }    

        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
