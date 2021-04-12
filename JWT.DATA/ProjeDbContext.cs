using JWT.CORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.DATA
{
    public class ProjeDbContext:IdentityDbContext<Kullanici,IdentityRole,string>
    {
        public ProjeDbContext(DbContextOptions<ProjeDbContext> options):base(options)
        {

        }
        DbSet<Products> product { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }

    }
}
