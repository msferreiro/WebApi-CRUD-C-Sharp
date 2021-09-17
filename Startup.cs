using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Data;
using TestWebApi.Data.Interfaces;
using TestWebApi.Mapper;
using TestWebApi.Services;
using TestWebApi.Services.Interfaces;

namespace TestWebApi
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
            services.AddDbContext<DataContext>(x =>
            {
                x.UseLazyLoadingProxies();
                x.UseSqlite (Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            services.AddControllers();

            // Configuramos la DI de la clase Repository
            // Scoped: se crean una vez por solicitud dentro del alcance.
            // Es equivalente a Singleton en el alcance actual.
            // p.ej. en MVC crea 1 instancia por cada solicitud http, pero usa la misma instancia en
            // las otras llamadas dentro de la misma solicitud web.
            services.AddScoped<IRepository, Repository>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            // Configuración para hacer el uso de Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>

                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token"])),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   }
               );
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITokenService, TokenService>();
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

            // Habilitamos uso de autenticación por token
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
