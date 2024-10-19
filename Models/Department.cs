namespace DentalClinic.Models
{
    public class Department
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdatedAt { get; set; }


        // Relation fields
        public int SpecializationId { get; set; }

        // Navigation Properties
        public IQueryable<Clinic> Clinics { get; set; }

        public Specialization Specilaization { get; set; }
    }
}
