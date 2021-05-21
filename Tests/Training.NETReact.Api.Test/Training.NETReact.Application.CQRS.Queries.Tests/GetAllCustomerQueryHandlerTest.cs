using Moq;
using NUnit.Framework;
using System.Linq;
using Training.NETReact.Application.CQRS.Queries.GetAllCustomer;
using Training.NETReact.Domain.Contracts;

namespace Training.NETReact.Api.Test.Training.NETReact.Application.CQRS.Queries
{
    [TestFixture]
    public class GetAllCustomerQueryHandlerTest  : InitializeCustomerData
    {
        private Mock<ICustomeRepository> mockRepository = new Mock<ICustomeRepository>();

        [Test]
        public void GetAllCustomerTest()
        {
            mockRepository.Setup(x => x.GetAllCustomers()).ReturnsAsync(CustomerData());

            GetAllCustomerQuery queryRequest = new GetAllCustomerQuery();
            GetAllCustomerQueryHandler queryHandler = new GetAllCustomerQueryHandler(mockRepository.Object);

            var customerResult = queryHandler.Handle(queryRequest, new System.Threading.CancellationToken());

            //assert
            Assert.IsTrue(customerResult.Result.ToList().Count() == 3);
            Assert.IsTrue(customerResult.Result.FirstOrDefault().FirstName == "Juan");
        }
    }
}
