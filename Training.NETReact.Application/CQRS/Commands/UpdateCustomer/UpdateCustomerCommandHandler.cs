using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Infrasctructure.Data;

namespace Training.NETReact.Application.CQRS.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ICustomeRepository _repo;
        protected readonly IMapper _mapper;

        private UpdateCustomerCommandHandler()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CustomerDto, Domain.Models.Customer>();
                cfg.CreateMap<Domain.Models.Customer, CustomerDto>();
            });

            _mapper = config.CreateMapper();
        }

        public UpdateCustomerCommandHandler(ICustomeRepository repository) : this() => _repo = repository;

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!EmailValidator.IsValidEmail(request.Customer.Email))
                request.Customer.Email = string.Empty;

            return _mapper.Map<CustomerDto>(await _repo.UpdateCustomer(_mapper.Map<Domain.Models.Customer>(request.Customer)));
        }
    }
}
