using BUGSystem.BL.Mangers.UserManager;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BUGSystem.BL
{
    public static class BLExtention
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBugManager, BugManager>();
            services.AddScoped<IAttachmentManager, AttachmentManager>();
           
            services.AddValidatorsFromAssembly(
                typeof(BLExtention).Assembly
                );
        }
    }
}
