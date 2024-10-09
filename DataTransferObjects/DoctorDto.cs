using DentalClinic.Models;

namespace DentalClinic.DataTransferObjects
{
    public class DoctorDto
    {
        public int Id { get; set; }

        public DateTime? JoiningDateTime { get; set; } = DateTime.Now;

        public DateOnly GraduationDate { get; set; }    

        public double YearsOfExperience { get; set; }   

        public string CurrentMajor {  get; set; }   

        public string UniversityName { get; set; }  

        public string? Notes { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } 

        // Relation fields
        public string UserId { get; set; }  

        // Navigation properties
        public IQueryable<WorkingHours> WorkingHours { get; set; }

        public IQueryable<Clinic> Clinics { get; set; }
    }
}
