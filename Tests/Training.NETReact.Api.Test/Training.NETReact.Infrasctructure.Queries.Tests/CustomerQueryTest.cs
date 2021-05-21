using Moq;
using NUnit.Framework;
using System.Linq;
using Training.NETReact.Infrasctructure.Queries;
using Training.NETReact.Infrasctructure.Data;
using Training.NETReact.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Training.NETReact.Api.Test.Training.NETReact.Infrasctructure.Queries.Tests
{
    [TestFixture]
    public class CustomerQueryTest : InitializeCustomerData
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
        public void GetCustomerByIdQueryTests()
        {          
            //arrange
            GetCustomerByIdQuery getByIdQuery = new GetCustomerByIdQuery(mockContext.Object, CustomerData().FirstOrDefault().Id);

            //Act
            var result = getByIdQuery.ExecuteQuery();

            //Assert
            Assert.IsTrue(result.IsCompletedSuccessfully);
        }

        [Test]
        public void DeleteCustomerQueryTest()
        {
            //arrange
            DeleteCustomerQuery deleteCustomerQuery = new DeleteCustomerQuery(mockContext.Object, CustomerData().FirstOrDefault().Id);

            //act
            var result = deleteCustomerQuery.ExecuteQuery();

            //assert
            Assert.IsTrue(result.Result == CustomerData().FirstOrDefault().Id);
        }

        [Test]
        public void AddCustomerQueryTest()
        {           
            //arrange
            AddCustomerQuery addCustomerQuery = new AddCustomerQuery(mockContext.Object, CustomerData().FirstOrDefault());

            //Act
            var result = addCustomerQuery.ExecuteQuery();

            //Assert
            Assert.IsTrue(result.Result.FirstName == CustomerData().FirstOrDefault().FirstName);
        }

        [Test]
        public void GetAllCustomerQueryTest()
        {     
            //arrange
            GetAllCustomerQuery fetchAllCustomer = new GetAllCustomerQuery(mockContext.Object);

            //act
            var result = fetchAllCustomer.ExecuteQuery();

            //assert
            Assert.IsTrue(result != null);
        }
       
    }
}
