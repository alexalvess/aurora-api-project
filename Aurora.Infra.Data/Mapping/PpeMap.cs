using Aurora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora.Infra.Data.Mapping
{
    public class PpeMap : IEntityTypeConfiguration<PersonalProtectiveEquipment>
    {
        public void Configure(EntityTypeBuilder<PersonalProtectiveEquipment> builder)
        {
            builder.ToTable(nameof(PersonalProtectiveEquipment));

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Description)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(prop => prop.Quantity)
               .IsRequired();

            builder.Property(prop => prop.ApprovalCertificate)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(prop => prop.ManufacturingDate)
               .IsRequired();

            builder.Property(prop => prop.Durability)
               .IsRequired();

            builder
                .HasMany(prop => prop.PpePossessions)
                .WithOne();
        }
    }
}
