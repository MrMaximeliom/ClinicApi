namespace DentalClinic.Models
{


    public class Doctor
    {
        public int Id { get; set; }

        public DateTime? JoiningDateTime { get; set; } = DateTime.Now;  

        public DateOnly GraduationDate { get; set; }    

        public double YearsOfExperience { get; set; }   

        public string CurrentMajor {  get; set; }  
        
        public string UniversityName { get; set; }  

        public string? Notes { get; set; }  

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set;} 


        // Relation fields
        public string UserId { get; set; } 

        // Navigation properties

        public User User { get; set; }

        public IQueryable<WorkingHours> WorkingHours { get; set; }


        public IQueryable<Clinic> Clinics { get; set; }



    }
}
