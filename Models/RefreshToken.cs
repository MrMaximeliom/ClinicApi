namespace DentalClinic.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } 

        public DateTime ExpiresOn { get; set; } 

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedOn { get; set; }

        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
