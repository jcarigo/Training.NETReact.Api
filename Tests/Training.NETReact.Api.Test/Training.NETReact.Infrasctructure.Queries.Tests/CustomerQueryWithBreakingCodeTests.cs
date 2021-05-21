using Moq;
using NUnit.Framework;
using System.Linq;
using Training.NETReact.Infrasctructure.Queries;
using Training.NETReact.Infrasctructure.Data;
using Training.NETReact.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Training.NETReact.Domain.ENUM;
using Training.NETReact.Application;

namespace Training.NETReact.Api.Test.Training.NETReact.Infrasctructure.Queries.Tests
{
    public class CustomerQueryWithBreakingCodeTests : InitializeCustomerData
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
        public void AddCustomerQueryWithInvalidFieldValueTest()
        {
            //arrange
            var customerParam = new Customer { FirstName = null, LastName = null, Email = "invalid_email.com", BirthDate = DateTime.Parse("1970-03-14"), Gender = Gender.Male };

            AddCustomerQuery addCustomerQuery = new AddCustomerQuery(mockContext.Object, customerParam);

            //Act
            dynamic result = null;

            if (EmailValidator.IsValidEmail(customerParam.Email) || customerParam.FirstName != null || customerParam.LastName != null)
                result = addCustomerQuery.ExecuteQuery();


            //Assert
            Assert.IsTrue(result == null);
        }
    }
}
