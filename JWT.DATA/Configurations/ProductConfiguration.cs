using JWT.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.DATA.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stok).IsRequired();
            builder.Property(x => x.Fiyat).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

        }
    }
}
