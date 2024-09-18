namespace DentalClinic.Models
{
/*    name
user_id
clinic_id
booking_date
booking_time*/

    public class Booking
    {
        public int Id { get; set; } 

        public string SerialNumber  { get; set; }   

        public DateOnly BookingDate { get; set; }   

        public TimeOnly BookingTime     { get; set; } 
        
        // Relation field
        public int ClinicId { get; set; }   

        public int UserId {  get; set; }    

        // Navigation properties
        public Clinic  Clinic { get; set; }

        public User User { get; set; }  
    }
}
