using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.RoleAgg;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "role");
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsMany(x => x.Permissions, option =>
        {
            option.ToTable("Permissions", "role");
        });
    }
}
