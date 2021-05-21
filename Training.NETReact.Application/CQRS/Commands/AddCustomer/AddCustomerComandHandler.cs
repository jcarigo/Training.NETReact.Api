using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.Contracts;

namespace Training.NETReact.Application.CQRS.Commands.AddCustomer
{
    public class AddCustomerComandHandler : IRequestHandler<AddCustomerCommand, CustomerDto>
    {
        private readonly ICustomeRepository _repo;
        private readonly IMapper _mapper;

        private AddCustomerComandHandler()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddCustomerCommand, Domain.Models.Customer>();
                cfg.CreateMap<Domain.Models.Customer, CustomerDto>();
            });

            _mapper = config.CreateMapper();
        }

        public AddCustomerComandHandler(ICustomeRepository repository) : this() => _repo = repository;

        public async Task<CustomerDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!EmailValidator.IsValidEmail(request.Email))
                request.Email = string.Empty;

            var customer = _mapper.Map<Domain.Models.Customer>(request);
            return _mapper.Map<CustomerDto>(await _repo.AddCustomer(customer));

        }
    }
}
