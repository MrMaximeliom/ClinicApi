using DentalClinic.Models;

namespace DentalClinic.DataTransferObjects
{
    public class DepartmentDto
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;   

        public DateTime? LastUpdatedAt { get; set; }  
        
        // Relation fields
        public int SpecializationId { get; set; }  
        
        // Navigation Properties

        public IQueryable<Clinic> Clinics { get; set; }

        public Specialization Specialization { get; set; }


    }
}
