
using BUGSystem.DAL;
using BUGSystem.BL;
using BUGSystem.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BUGSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MyContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

            builder.Services.AddAccessLayer(builder.Configuration); 
            builder.Services.AddBusinessLayer();

            //builder.Services.AddIdentity<User, IdentityRole<Guid>>()
            //    .AddEntityFrameworkStores<MyContext>()
            //.AddDefaultTokenProviders();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
            {
              options.TokenValidationParameters = new TokenValidationParameters
            {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
             };
            });

            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddAuthorizationPolicies();

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
            //    options.AddPolicy("DeveloperPolicy", policy => policy.RequireRole("Developer"));
            //    options.AddPolicy("TesterPolicy", policy => policy.RequireRole("Tester"));
            //});

            ///////////////////////////////////////////////////////////
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            


            app.MapControllers();

            app.Run();
        }
    }
}
