namespace DentalClinic.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedAt { get; set; }    = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } 
        
        // Navigation properties
        public IQueryable<Department> Departments { get; set; } 


    }
}
