namespace DentalClinic.Models
{
    public class AddRole(string userId, string Role)
    {
        public string UserId { get; set; } = userId;

        public string Role { get; set; } = Role;
    }
}
