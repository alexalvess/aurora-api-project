using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.Domain.Entities;

namespace Aurora.Infra.Data.Mapping
{
    public class WorkerMap : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Worker");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Nin)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("NationalInsuranceNumber")
                .HasColumnType("varchar(11)");

            builder.Property(prop => prop.BirthDate)
                .IsRequired()
                .HasColumnName("BirthDate");

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Password)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("varchar(100)");

            builder
                .HasMany(prop => prop.PpePossessions)
                .WithOne();
        }
    }
}
