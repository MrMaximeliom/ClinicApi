using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class DoctorEntityTypeConfiguration:IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Configure Id properties
            builder
                .Property(z => z.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            // Configure UserId properties
            builder
                .Property(r => r.UserId)
                .IsRequired();
            // Configure JoiningDateTime properties
            builder
                .Property(d => d.JoiningDateTime)
                .IsRequired(false);

            // Configure GraduationDate properties
            builder
                .Property(x => x.GraduationDate)
                .IsRequired();

            // Configure YearsOfExperience properties
            builder
                .Property(s => s.YearsOfExperience)
                .IsRequired()
                .HasPrecision(3,1);

            // Configure CurrentMajor properties
            builder
                .Property(c => c.CurrentMajor)
                .IsRequired()
                .HasMaxLength(100);

            // Configure UniversityName properties
            builder 
                .Property(t => t.UniversityName)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Notes properties
            builder
                .Property(r => r.Notes)
                .IsRequired(false)
                .HasMaxLength(300);

            // Configure CreatedAt properties
            builder 
                .Property(r => r.CreatedAt)
                .IsRequired(false); 
            // Configure LastUpdatedAt properties
            builder 
                .Property(e => e.LastUpdatedAt)
                .IsRequired(false)
                .ValueGeneratedOnUpdate()
                .HasDefaultValueSql("GETDATE()");


        }
    }
}
