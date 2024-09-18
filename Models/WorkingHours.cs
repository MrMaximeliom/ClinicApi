namespace DentalClinic.Models
{
    public class WorkingHours
    {
        public int Id { get; set; } 

        public int ClinicId { get; set; }
        public int DoctorId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }    

        public TimeSpan OpenTime { get; set; }  

        public TimeSpan CloseTime { get; set; } 

        // Navigation property
        public Clinic Clinic { get; set; }

        public Doctor Doctor { get; set; }
    }
}
