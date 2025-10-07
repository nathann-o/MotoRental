using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotoRental.Infrastructure.Persistence.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rentals");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Plan)
                .HasConversion<int>() 
                .IsRequired();

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.ExpectedEndDate)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.IsActive)
                .IsRequired();

            
            builder.HasOne<Rider>()         
                   .WithMany()             
                   .HasForeignKey(r => r.RiderId)
                   .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasOne<Motorcycle>()     
                   .WithMany()              
                   .HasForeignKey(r => r.MotorcycleId) 
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
