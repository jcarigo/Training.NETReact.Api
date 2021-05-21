using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Training.NETReact.Application.CQRS.Queries.GetAllCustomer;
using Training.NETReact.Application.CQRS.Queries.GetCustomerById;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.ENUM;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Api.Test.Training.NETReact.Application.CQRS.Queries
{
    [TestFixture]
    public class GetCustomerByIdQueryHandlerTest : InitializeCustomerData
    {
        private Mock<ICustomeRepository> mockRepository = new Mock<ICustomeRepository>();

        [Test]
        public void GetCustomerByIdTest()
        {
            int customerId = 2;

            mockRepository.Setup(x => x.GetCustomerById(customerId)).ReturnsAsync(CustomerData().Find(x => x.Id == customerId));
            
            GetCustomerByIdQuery queryRequest = new GetCustomerByIdQuery();
            GetCustomerByIdQueryHandler requestHandler = new GetCustomerByIdQueryHandler(mockRepository.Object);

            queryRequest.Id = customerId;

            var customerResult = requestHandler.Handle(queryRequest, new System.Threading.CancellationToken());

            //assert
            Assert.AreEqual(customerResult.Result.FirstName, "Maria Claria");
            Assert.AreEqual(customerResult.Result.Gender, Gender.Female);
                        
        }
    }
}
