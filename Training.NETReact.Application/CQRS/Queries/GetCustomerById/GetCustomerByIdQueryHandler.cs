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

namespace Training.NETReact.Application.CQRS.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomeRepository _repo;
        protected readonly IMapper _mapper;

        private GetCustomerByIdQueryHandler()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Domain.Models.Customer, CustomerDto>();
            });

            _mapper = config.CreateMapper();
        }

        public GetCustomerByIdQueryHandler(ICustomeRepository repository) : this() => _repo = repository;

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CustomerDto>(await _repo.GetCustomerById(request.Id));
        }
    }
}
