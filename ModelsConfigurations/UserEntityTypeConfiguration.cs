using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class UserEntityTypeConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // configure names properties
            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(y => y.SecondName)
                .IsRequired()   
                .HasMaxLength(20);  

            builder 
                .Property(t => t.ThirdName) 
                .IsRequired()
                .HasMaxLength(20);

            builder 
                .Property(t => t.FourthName)
                .IsRequired()
                .HasMaxLength(20);  

            // Configure email properties
            builder
                .Property(e => e.Email)
                 .IsRequired(false)
                .HasMaxLength(40);

            // Configure phone number properties
            builder
                .Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            // Configure username properties
            builder
                .Property (s => s.UserName)
                .IsRequired(false).
                HasMaxLength(25);   
        }
    }
}
