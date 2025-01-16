using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CRUDDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CRUDDBContext"),
              b => b.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName)),
              ServiceLifetime.Scoped);

            services.AddTransient<IUserRepositories, UserRepositories>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
