using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthApiExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApiExample.Sistema6sData.Configurations
{
    class Departamentos6sConfiguration : IEntityTypeConfiguration<Departamentos6s>
    {
        public void Configure(EntityTypeBuilder<Departamentos6s> builder)
        {
            builder.HasKey(e => e.DepartamentoId);

            builder.ToTable("departamentos_6s");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Area)
                .WithMany(p => p.Departamentos6s)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__departame__AreaI__4AB81AF0");
        }
    }
}
