using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class ClinicEntityTypeConfiguration:IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            // Configure Id properties
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure DepartmentId properties
            builder
                .Property(x => x.DepartmentId)
                .IsRequired();

            // Configure Name properties
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(70);
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
