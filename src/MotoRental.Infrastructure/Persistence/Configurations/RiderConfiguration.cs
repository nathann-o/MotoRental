using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotoRental.Infrastructure.Persistence.Configurations
{
    public class RiderConfiguration : IEntityTypeConfiguration<Rider>
    {
        public void Configure(EntityTypeBuilder<Rider> builder)
        {
            builder.ToTable("riders");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Cnpj)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.BirthDate)
                .IsRequired();

            builder.Property(r => r.CnhNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.CnhType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(r => r.CnhImageUrl)
                .HasMaxLength(500); 


            builder.HasMany<Rental>()
                   .WithOne()
                   .HasForeignKey(r => r.RiderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
