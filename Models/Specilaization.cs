namespace DentalClinic.Models
{
    public class Specilaization (string name)
    {
        public int Id { get; set; }

        public string Name { get; set; } = name;

       

        // Navigation properties
        public IQueryable<Department> Departments { get; set; } 


    }
}
