using Moq;
using NUnit.Framework;
using System.Linq;
using Training.NETReact.Application.CQRS.Commands.DeleteCustomer;
using Training.NETReact.Domain.Contracts;

namespace Training.NETReact.Api.Test.Training.NETReact.Application.CQRS.Commands.Tests
{
    [TestFixture]
    public class DeleteCustomerCommandHandlerTest : InitializeCustomerData
    {
        private Mock<ICustomeRepository> mockRepository = new Mock<ICustomeRepository>();

        [Test]
        public void DeleteCustomerTest()
        {
            mockRepository.Setup(x => x.GetAllCustomers()).ReturnsAsync(CustomerData());

            DeleteCustomerCommand command = new DeleteCustomerCommand();
            command.Id = 3; 

            DeleteCustomerCommandHandler commandHandler = new DeleteCustomerCommandHandler(mockRepository.Object);

            var result = commandHandler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result.IsCompletedSuccessfully);
        }
    }
}
