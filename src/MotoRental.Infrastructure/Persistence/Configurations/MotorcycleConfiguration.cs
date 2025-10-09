using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotoRental.Infrastructure.Persistence.Configurations
{
    public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable("motorcycles");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd();

            builder.Property(v => v.Model)
                .IsRequired()
                .HasMaxLength(100);

            //builder.Property(v => v.Plate)
            //    .HasConversion(
            //        plate => plate.Value,
            //        value => Plate.Create(value)
            //    )
            //    .IsRequired()
            //    .HasMaxLength(10);

            builder.OwnsOne(m => m.Plate, plate =>
            {
                plate.Property(p => p.Value)
                    .HasColumnName("Plate")
                    .IsRequired();
            });

            builder.Property(v => v.Year)
                .IsRequired();
        }
    }
}
