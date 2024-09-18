namespace DentalClinic.Models
{
    public enum UserType
    {
        Admin,
        Support,
        Staff,
        Doctor,
        Patient
    }

    public static class UserTypeMappings
    {
        private static readonly Dictionary<UserType, string> RoleDisplayNames = new()
        {
            {UserType.Admin ,"Administrator"},
            {UserType.Support,"Support Member" },
            {UserType.Staff,"Staff Member" },
            {UserType.Doctor,"Doctor Member" },
            {UserType.Patient,"Patient Member" }
        };
        public static string GetDisplayName(UserType type)
        {
            return RoleDisplayNames.TryGetValue(type, out var displayName) ? displayName : type.ToString();
        }

    }


}
