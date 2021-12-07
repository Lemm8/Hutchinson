using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthApiExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApiExample.Sistema6sData.Configurations
{
    class Auditorias6sConfiguration : IEntityTypeConfiguration<Auditorias6s>
    {
        public void Configure(EntityTypeBuilder<Auditorias6s> builder)
        {
            builder.HasKey(e => e.AuditoriaId);

            builder.ToTable("auditorias_6s");

            builder.Property(e => e.FechaCompleto).HasColumnType("datetime");

            builder.Property(e => e.FechaInicio).HasColumnType("datetime");

            builder.Property(e => e.FechaTarget).HasColumnType("datetime");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.Area)
                .WithMany(p => p.Auditorias6s)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK__auditoria__AreaI__48CFD27E");

            builder.HasOne(d => d.Auditor)
                .WithMany(p => p.Auditorias6s)
                .HasForeignKey(d => d.AuditorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__auditoria__Audit__47DBAE45");

            builder.HasOne(d => d.Calificacion)
                .WithMany(p => p.Auditorias6s)
                .HasForeignKey(d => d.CalificacionId)
                .HasConstraintName("FK__auditoria__Calif__49C3F6B7");

            builder.Property(e => e.mes).HasColumnType("int");
        }
    }
}
