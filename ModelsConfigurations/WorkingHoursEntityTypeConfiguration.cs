using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class WorkingHoursEntityTypeConfiguration : IEntityTypeConfiguration<WorkingHours>
    {
        public void Configure(EntityTypeBuilder<WorkingHours> builder)
        {
            // Configure Id properties
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure ClinicId properties
            builder
                .Property(d => d.ClinicId)
                .IsRequired();

            // Configure DoctorId properties
            builder
                .Property(t => t.DoctorId)
                .IsRequired();

            // Configure DayOfWeek properties
            builder
                .Property(t => t.DayOfWeek)
                .IsRequired();

            // Configure OpenTime properties
            builder 
                .Property(r => r.OpenTime)
                .IsRequired();

            // Configure CloseTime properties
            builder 
                .Property(r => r.CloseTime)
                .IsRequired();
        }
    }
}
