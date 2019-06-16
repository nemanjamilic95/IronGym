using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class ProgramConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.HasIndex(p => p.Heading).IsUnique();
            builder.Property(p => p.Heading).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.Text).HasMaxLength(650).IsRequired();
            builder.Property(p => p.Picture).IsRequired();
        }
    }
}
