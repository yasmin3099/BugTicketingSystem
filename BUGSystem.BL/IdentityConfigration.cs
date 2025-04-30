using BUGSystem.DAL;
using BUGSystem.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BUGSystem.BL
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(ConfigureIdentityOptions)
                .AddEntityFrameworkStores<MyContext>()
            .AddDefaultTokenProviders();


            //// Configure JWT authentication
            //ConfigureJwtAuthentication(services, configuration);

            return services;
        }

        private static void ConfigureIdentityOptions(IdentityOptions options)
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // SignIn settings
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        }



       
        //Add Authorization policies
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.Admin,
                    policy => policy.RequireRole(Constants.Policies.Admin)
                    //.RequireClaim(ClaimTypes.NameIdentifier)
                    );
                options.AddPolicy(Constants.Policies.Manager,
                    policy => policy.RequireRole(Constants.Policies.Manager)
                    );
                options.AddPolicy(Constants.Policies.Developer,
                    policy => policy.RequireRole(Constants.Policies.Developer)
                    );
                options.AddPolicy(Constants.Policies.Tester,
                    policy => policy.RequireRole(Constants.Policies.Tester)
                    );
            });
        }
    }

}
