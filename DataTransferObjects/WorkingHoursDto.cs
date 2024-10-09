using DentalClinic.Models;


namespace DentalClinic.DataTransferObjects
{
    public class WorkingHoursDto
    {
        public int Id { get; set; } 

        public int ClinicId { get; set; }   

        public int DoctorId     { get; set; }   

        public Models.DayOfWeek DayOfWeek { get; set; }

        public TimeOnly OpenTime { get; set; }  

        public TimeOnly CloseTime { get; set; }

        // Navigation Properties
        public Clinic Clinic { get; set; }

        public Doctor Doctor { get; set; }
    }
}
