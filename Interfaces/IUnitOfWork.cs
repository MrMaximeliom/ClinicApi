using DentalClinic.Models;

namespace DentalClinic.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }

        IBaseRepository<Booking> Bookings { get; }

        IBaseRepository<Specialization> Specializations { get; }    

        IBaseRepository<Clinic> Clinics { get; }
        
        IBaseRepository<Department> Departments { get; }    

        IBaseRepository<Doctor> Doctors { get; }

        IBaseRepository<WorkingHours> WorkingHours { get; }
        int Complete();
    }
}
