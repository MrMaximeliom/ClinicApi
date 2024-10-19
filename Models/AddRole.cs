namespace DentalClinic.Models
{
    public class AddRole
    {
        public string UserId { get; set; }

        public string Role { get; set; }

        public AddRole(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
        public AddRole() { }
    }
}
