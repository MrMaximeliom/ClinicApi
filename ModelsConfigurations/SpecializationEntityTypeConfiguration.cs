using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class SpecializationEntityTypeConfiguration:IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure name properties
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
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
