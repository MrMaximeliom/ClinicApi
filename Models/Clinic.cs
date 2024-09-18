namespace DentalClinic.Models
{
/*    name
id
department_id
doctor_id
working_days*/
    public enum DayOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,

    }
    public class Clinic(string name)
    {
        public int Id { get; set; } 
        public string Name { get; set; } = name;

        // Relation fields
        public int DepartmentId { get; set; }   

        public ICollection<WorkingHours> WorkingHours { get; set; } = [];  

        // Navigation Properties
        public Department Department { get; set; }  

 
    }
}
