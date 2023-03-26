using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Assembly.GetExecutingAssembly()
            // obtain a reference to the assembly that contains the code that is currently executing

            // needed to get the AutoMapper profiles defined in this assembly
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // needed to get the MediatR handlers defined in this assembly
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
