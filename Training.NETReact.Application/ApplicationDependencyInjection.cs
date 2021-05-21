using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Training.NETReact.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationDependencyInjection));
            return services;
        }
    }
}
