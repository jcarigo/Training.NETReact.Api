using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Training.NETReact.Application.CQRS.Commands.AddCustomer;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.ENUM;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Api.Test.Training.NETReact.Application.CQRS.Commands.Tests
{
    [TestFixture]
    public class AddCustomerComandHandlerTest : InitializeCustomerData
    {
        private Mock<ICustomeRepository> mockRepository = new Mock<ICustomeRepository>();

        [Test]
        public void AddCustomerTest()
        {
            //Arrange
            var customerParam = new Customer { 
                FirstName = "Tom", 
                LastName = "Jones", 
                Email = "tjones@hotmail.com", 
                BirthDate = DateTime.Parse("1970-03-14"), 
                Gender = Gender.Male 
            };

            mockRepository.Setup(x => x.AddCustomer(CustomerData().FirstOrDefault())).ReturnsAsync(CustomerData().Find(x => x.Id == 1));

            AddCustomerCommand command = new AddCustomerCommand();
            command.FirstName = customerParam.FirstName;
            command.LastName = customerParam.LastName;
            command.BirthDate = customerParam.BirthDate;
            command.Email = customerParam.Email;
            command.Gender = customerParam.Gender;

            AddCustomerComandHandler commandHandler = new AddCustomerComandHandler(mockRepository.Object);

            //Act
            var dataResult = commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            Assert.IsTrue(dataResult != null);
        }
    }
}
