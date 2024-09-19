using DentalClinic.Interfaces;
using DentalClinic.Models;

namespace DentalClinic.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<User> Users { get; private set; }

        public IBaseRepository<Booking> Bookings { get; private set; }

        public IBaseRepository<Doctor> Doctors { get; private set; }

        public IBaseRepository<Clinic> Clinics { get; private set; }

        public IBaseRepository<WorkingHours> WorkingHours { get; private set; }

        public IBaseRepository<Department> Departments { get; private set; }

        public IBaseRepository<Specialization> Specializations { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new BaseRepository<User>(_context);
            Bookings = new BaseRepository<Booking>(_context);
            Doctors = new BaseRepository<Doctor>(_context); 
            Clinics = new BaseRepository<Clinic>(_context); 
            WorkingHours = new BaseRepository<WorkingHours>(_context);
            Departments = new BaseRepository<Department>(_context); 
            Specializations = new BaseRepository<Specialization>(_context); 
        }   

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();

    }
}
