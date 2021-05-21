using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Application.CQRS.Commands.AddCustomer;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<AddCustomerCommand,Customer>();
        }
    }
}
