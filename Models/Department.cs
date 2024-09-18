namespace DentalClinic.Models
{
    public class Department(string name)
    {
        public int Id { get; set; } 
        public string Name { get; set; } = name;

        // Relation fields
        public int SpecializationId { get; set; }

        // Navigation PRoperties
        public IQueryable<Clinic> Clinics { get; set; }

        public Specilaization Specilaization { get; set;
    }
}
