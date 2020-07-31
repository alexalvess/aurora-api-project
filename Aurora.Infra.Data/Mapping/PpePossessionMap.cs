using Aurora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora.Infra.Data.Mapping
{
    public class PpePossessionMap : IEntityTypeConfiguration<PpePossession>
    {
        public void Configure(EntityTypeBuilder<PpePossession> builder)
        {
            builder.ToTable("PersonalProtectiveEquipmentPossession");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.DeliveryDate)
               .IsRequired();

            builder.Property(prop => prop.ReturnDate);

            builder.Property(prop => prop.Confirmation)
               .IsRequired()
               .HasDefaultValue(false);

            builder
                .HasOne(prop => prop.PersonalProtectiveEquipment)
                .WithMany(prop => prop.PpePossessions)
                .HasForeignKey(prop => prop.PersonalProtectiveEquipmentId);

            builder
                .HasOne(prop => prop.Worker)
                .WithMany(prop => prop.PpePossessions)
                .HasForeignKey(prop => prop.WorkerId);
        }
    }
}
