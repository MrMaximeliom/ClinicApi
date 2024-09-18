using DentalClinic.Models;
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

        public DbSet<Specilaization> Specilaizations { get; set; }
        
        public DbSet<Department> Departments { get; set; }  

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


            builder.Entity<Clinic>()
                .HasMany(c => c.WorkingHours)
                .WithOne(wh => wh.Clinic)
                .HasForeignKey(c => c.ClinicId);

            builder.Entity<Clinic>()
                .HasOne(c => c.Department)
                .WithMany(c => c.Clinics)
                .HasForeignKey(x => x.DepartmentId);   
            
            builder.Entity<Doctor>()
                .HasMany(c => c.WorkingHours)
                .WithOne(wh => wh.Doctor)
                .HasForeignKey(c => c.DoctorId);

            builder.Entity<User>()
                .HasOne(c => c.Doctor)
                .WithOne(f => f.User)
                .HasForeignKey<Doctor>(p => p.UserId);

            builder.Entity<Specilaization>()
                .HasMany(p => p.Departments)
                .WithOne(p => p.Specilaization)
                .HasForeignKey(p => p.SpecializationId);




            

        }

    }
}
