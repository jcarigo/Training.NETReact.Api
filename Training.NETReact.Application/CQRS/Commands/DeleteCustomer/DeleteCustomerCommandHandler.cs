using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Training.NETReact.Domain.Contracts;

namespace Training.NETReact.Application.CQRS.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly ICustomeRepository _repo;
        public DeleteCustomerCommandHandler(ICustomeRepository repository) => _repo = repository;

        public Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return _repo.DeleteCustomer(request.Id);
        }
    }
}
