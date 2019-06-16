using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Username).HasMaxLength(20).IsRequired();
            builder.Property(u => u.RoleId).IsRequired();
            builder.Property(u=>u.Password).HasMaxLength(20).IsRequired();
            builder.Property(u=>u.LastName).HasMaxLength(20).IsRequired();
            builder.Property(u=>u.FirstName).HasMaxLength(20).IsRequired();
            builder.Property(u=>u.Email).HasMaxLength(40).IsRequired();
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.Avatar).HasMaxLength(255).IsRequired();
        }
    }
}
