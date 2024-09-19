namespace DentalClinic.Models
{
    public class WorkingHours
    {
        public int Id { get; set; } 

        public int ClinicId { get; set; }
        public int DoctorId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }    

        public TimeOnly OpenTime { get; set; }  

        public TimeOnly CloseTime { get; set; } 

        // Navigation property
        public Clinic Clinic { get; set; }

        public Doctor Doctor { get; set; }
    }
}
