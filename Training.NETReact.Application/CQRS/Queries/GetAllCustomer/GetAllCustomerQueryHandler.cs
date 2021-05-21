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

namespace Training.NETReact.Application.CQRS.Queries.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomeRepository _repo;
        protected readonly IMapper _mapper;

        private GetAllCustomerQueryHandler()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Domain.Models.Customer, CustomerDto>();
            });

            _mapper = config.CreateMapper();
        }

        public GetAllCustomerQueryHandler(ICustomeRepository repository) : this() => _repo = repository;

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CustomerDto>>(await _repo.GetAllCustomers());
        }
    }
}
