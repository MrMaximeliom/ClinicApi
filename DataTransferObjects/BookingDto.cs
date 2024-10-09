using DentalClinic.Models;

namespace CashierApi.DataTransferObjects
{
    public class BookingDto
    {
        public int Id { get; set; }     

        public string SerialNumber { get; set; }

        public DateOnly BookingDate { get; set; }

        public TimeOnly BookingTime { get; set; }


        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        // Relation fields
        public int ClinicId { get; set; }

        public string UserId    { get; set; }

        // Navigation properties

        public Clinic Clinic { get; set; }

        public User User { get; set; }


    }
}
