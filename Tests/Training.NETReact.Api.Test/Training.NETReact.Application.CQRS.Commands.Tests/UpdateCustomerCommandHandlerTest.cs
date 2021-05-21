using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Training.NETReact.Application.CQRS.Commands.UpdateCustomer;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.ENUM;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Api.Test.Training.NETReact.Application.CQRS.Commands.Tests
{
    [TestFixture]
    public class UpdateCustomerCommandHandlerTest : InitializeCustomerData
    {
        private Mock<ICustomeRepository> mockRepository = new Mock<ICustomeRepository>();

        [Test]
        public void UpdateCustomerTest() 
        {
            var customerParam = new CustomerDto
            {
                Id = 1,
                FirstName = "Tom",
                LastName = "Jones",
                Email = "tjones@hotmail.com",
                BirthDate = DateTime.Parse("1970-03-14"),
                Gender = Gender.Male
            };

            mockRepository.Setup(x => x.UpdateCustomer(CustomerData().FirstOrDefault())).ReturnsAsync(CustomerData().Find(x => x.Id == 1));
            UpdateCustomerCommand command = new UpdateCustomerCommand();
            command.Customer = customerParam;

            UpdateCustomerCommandHandler commandHandler = new UpdateCustomerCommandHandler(mockRepository.Object);

            //Act
            var dataResult = commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            Assert.IsTrue(dataResult.IsCompletedSuccessfully);
        }
    }
}
