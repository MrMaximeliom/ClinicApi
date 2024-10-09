using DentalClinic.Models;

namespace DentalClinic.DataTransferObjects
{
    public class ClinicDto
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        // Relation fields
        public int DepartmentId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdatedAt { get; set; }

        public ICollection<WorkingHours> WorkingHours { get; set; }

        // Navigation Properties

        public Department Department { get; set; }
    }
}
