using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Training.NETReact.Application.CQRS.Commands.AddCustomer;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.ENUM;
using Training.NETReact.Domain.Models;
using Training.NETReact.Infrasctructure.Data;
using Training.NETReact.Infrasctructure.Repository;

namespace Training.NETReact.Api.Test
{
    [TestFixture]
    public class CustomerRepositoryTests : InitializeCustomerData
    {
        private Mock<ICustomerDataContext> mockContext = new Mock<ICustomerDataContext>();
        private Mock<DbSet<Customer>> mockDbSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Customer>>();

        [SetUp]
        public void Init()
        {
            mockContext.Setup(x => x.GetCustomerDataContext());

            foreach (var data in CustomerData())
            {
                mockDbSet.Setup(x => x.Add(data));
            }

            mockContext.Setup(x => x.Customer).Returns(mockDbSet.Object);
        }

        [Test]
        public void GetAllCustomersTest()
        {
            CustomerRepository repo = new CustomerRepository(mockContext.Object);

            var result = repo.GetAllCustomers();

            Assert.IsTrue(result != null);
        }

    }
}
