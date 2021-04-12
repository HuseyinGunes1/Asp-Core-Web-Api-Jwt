using JWT.CORE.Configuration;
using JWT.CORE.Models;
using JWT.CORE.Repository;
using JWT.CORE.Services;
using JWT.DATA;
using JWT.DATA.Repositories;
using JWT.SERVÝCE.Services;
using JWT.SHARED.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Configuration ile appsettinjsona eriþebilirim 
            
            services.AddScoped<IAuthenticationService, AuthenticationServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenServices, CustomTokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServicesGeneric<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ProjeDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("JWT.DATA");
                });
            });

            services.AddIdentity<Kullanici, IdentityRole>(Opt =>
            {
                Opt.User.RequireUniqueEmail = true;
                Opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ProjeDbContext>().AddDefaultTokenProviders();

            services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenAyar"));//CustomTokenOptionsu git appsettingjsondan al
            services.Configure<Client>(Configuration.GetSection("Clients"));
           

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptionss = Configuration.GetSection("TokenAyar").Get<CustomTokenOptions>();
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptionss.Issuer,
                    ValidAudience=tokenOptionss.Audience[0],
                    IssuerSigningKey=SignService.SimetrikAnahtar(tokenOptionss.SecuritKey),

                    ValidateIssuerSigningKey=true,
                    ValidateAudience=true,
                    ValidateIssuer=true,
                    ClockSkew=TimeSpan.Zero


                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
