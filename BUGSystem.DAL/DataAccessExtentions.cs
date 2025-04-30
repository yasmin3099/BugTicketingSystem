using BUGSystem.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Net;

namespace BUGSystem.DAL
{
    public  static class DataAccessExtentions
    {
        public static void AddAccessLayer(
                this IServiceCollection services,
                IConfiguration configuration
                )
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<MyContext>(options => {
                options.UseSqlServer(connectionString);
            });

            


            services.AddScoped<IBugRepo, BugRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IBugUserRepo, BugUserRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IAttachmentRepo, AttachmentRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
        }
    }
}
