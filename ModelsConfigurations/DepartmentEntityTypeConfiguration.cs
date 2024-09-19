using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class DepartmentEntityTypeConfiguration:IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Configure Id properties
            builder
                .Property(w => w.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure SpecializationId properties
            builder
                .Property(w => w.SpecializationId)
                .IsRequired();

            // Configure Name properties
            builder
                .Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);

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
