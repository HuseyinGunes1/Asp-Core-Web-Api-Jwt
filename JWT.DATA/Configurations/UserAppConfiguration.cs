using JWT.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.DATA.Configurations
{
   public class UserAppConfiguration:IEntityTypeConfiguration<Kullanici>
    {
        

        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.City).HasMaxLength(50);
        }
    }
}
