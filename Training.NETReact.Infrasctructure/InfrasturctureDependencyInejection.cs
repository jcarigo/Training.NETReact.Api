using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Infrasctructure.Data;
using Training.NETReact.Infrasctructure.Repository;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Infrasctructure;

namespace Training.NETReact.Infrasctructure
{
    public static class InfrasturctureDependencyInejection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            string conn = configuration.GetConnectionString("CustomerDB");

            services.AddDbContext<CustomerDataContext>(buidler => buidler.UseSqlServer(conn));

            services.AddScoped<ICustomerDataContext, CustomerDataContext>();
            services.AddScoped<ICustomeRepository, CustomerRepository>();

            return services;
        }
    }
}

