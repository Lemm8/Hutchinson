using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthApiExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApiExample.Sistema6sData.Configurations
{
    class Auditores6sConfiguration : IEntityTypeConfiguration<Auditores6s>
    {
        public void Configure(EntityTypeBuilder<Auditores6s> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.ToTable("auditores_6s");

            builder.Property(e => e.FechaNacimiento).HasColumnType("datetime");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
