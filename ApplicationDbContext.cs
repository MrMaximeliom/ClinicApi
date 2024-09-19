using DentalClinic.Models;
using DentalClinic.ModelsConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<User> Users {  get; set; }     
        public DbSet<Clinic> Clinics { get; set; }   

        public DbSet<Doctor> Doctors { get; set; }  

        public DbSet<Specialization> Specilaizations { get; set; }
        
        public DbSet<Department> Departments { get; set; }  

        public DbSet<Booking> Bookings { get; set; }    

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("userRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("userTokens");

            // Configure Clinic has many WorkingHours
            builder.Entity<Clinic>()
                .HasMany(c => c.WorkingHours)
                .WithOne(wh => wh.Clinic)
                .HasForeignKey(c => c.ClinicId);

            // Configure Clinic has one Department
            builder.Entity<Clinic>()
                .HasOne(c => c.Department)
                .WithMany(c => c.Clinics)
                .HasForeignKey(x => x.DepartmentId);   

            // Configure Doctor has many WorkingHours
            builder.Entity<Doctor>()
                .HasMany(c => c.WorkingHours)
                .WithOne(wh => wh.Doctor)
                .HasForeignKey(c => c.DoctorId);

            // Configure User has one Doctor
            builder.Entity<User>()
                .HasOne(c => c.Doctor)
                .WithOne(f => f.User)
                .HasForeignKey<Doctor>(p => p.UserId);

            // Configure Specilization has many Departments
            builder.Entity<Specialization>()
                .HasMany(p => p.Departments)
                .WithOne(p => p.Specilaization)
                .HasForeignKey(p => p.SpecializationId);

            // Configure User properties
            new UserEntityTypeConfiguration().Configure(builder.Entity<User>());

            // Configure Specialization properties
            new SpecializationEntityTypeConfiguration().Configure(builder.Entity<Specialization>());

            // Configure Doctor properties
            new DoctorEntityTypeConfiguration().Configure(builder.Entity<Doctor>());    

            // Configure Department properties
            new DepartmentEntityTypeConfiguration().Configure(builder.Entity<Department>());    

            // Configure Clinic properties
            new ClinicEntityTypeConfiguration().Configure(builder.Entity<Clinic>());    

            // Configure Booking properties
            new BookingEntityTypeConfiguration().Configure(builder.Entity<Booking>());  

        }

    }
}
