using ClothesDataMicroservice.Logic.Repositories;
using ClothesDataMicroservice.Logic.Security;
using ClothesDataMicroservice.Model.DataBase;
using ClothesDataMicroservice.Model.IRepositories;
using ClothesDataMicroservice.Rest.Application.Queries;
using ClothesDataMicroservice.Rest.Commands;
using ClothesDataMicroservice.Rest.CommandsHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace ClothesDataMicroservice.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services
              .AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = true;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("something-blablabla-i-will-do-it-later-or-never")),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        RequireExpirationTime = false,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true
                    }; 
                });
            services.AddSingleton<ITokenBuilder, JwtTokenBuilder>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IClothesRepository, ClothesRepository>();
            services.AddScoped<IClothesQueriesHandler, ClothesQueriesHandler>();
            services.AddScoped<ICommandHandler<AddUserCommand>, UsersCommandsHandler>();
            services.AddControllers();
            using (var context = new DatabaseContext())
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      
    }
}
