using ChallengeAPI.Entities;
using ChallengeAPI.Infra;
using ChallengeAPI.UserDbContextContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAPI
{
    public class Startup
    {
        private object options;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(options =>
            { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Zomato API",
            //        Version = "v1",
            //        Description = "Description for the API goes here.",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Ankush Jain",
            //            Email = string.Empty,
            //            Url = new Uri("https://coderjony.com/"),
            //        },
            //    });
            //});
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Challenge.PreAceleracion", Version = "v1" });

             });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContextContex>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(configureOptions: Options =>
             {
                 Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 Options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             })
             .AddJwtBearer(options =>
             {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = "https://localhost:5001",
                     ValidIssuer = "https://localhost:5001",
                     IssuerSigningKey =
                          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: "KeySecretaSuperLargaDeAUTORIZACION"))

                 };
             });
            
            

            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<DbContext>(optionsAction:(i, options) =>
            {
                options.UseInternalServiceProvider(i);
                options.UseSqlServer(Configuration.GetConnectionString(name:"ChallengeConectionString"));
            });

            services.AddDbContext<UserDbContextContex>(optionsAction:(IServiceProvider, options) =>
            {
                options.UseInternalServiceProvider(IServiceProvider);
                options.UseSqlServer(Configuration.GetConnectionString("UserConnectionString"));
            });

        }

        

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zomato API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentication}/{action=Register}/{id?}");
            });
        }
    }
}
