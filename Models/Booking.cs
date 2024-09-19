namespace DentalClinic.Models
{

    public class Booking
    {
        public int Id { get; set; } 

        public string SerialNumber  { get; set; }   

        public DateOnly BookingDate { get; set; }   

        public TimeOnly BookingTime     { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdatedAt { get; set; }

        // Relation field
        public int ClinicId { get; set; }   

        public string UserId {  get; set; }    

        // Navigation properties
        public Clinic  Clinic { get; set; }

        public User User { get; set; }  
    }
}
