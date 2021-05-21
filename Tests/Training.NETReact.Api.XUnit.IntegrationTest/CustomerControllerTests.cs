using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Training.NETReact.Application.CQRS.Commands.AddCustomer;
using Training.NETReact.Application.CQRS.Commands.UpdateCustomer;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.ENUM;
using Xunit;

namespace Training.NETReact.Api.XUnit.IntegrationTest
{
    public class CustomerControllerTests : AppInstance 
    {
        private CustomerDto customer;

        [Fact]
        public async Task GetAllCustomer_Should_Return_CustomerList_IfNotEmpty()
        {
            //ARRANGE
            await AuthenticateAsync();

            //ACT
            var response = await TestClient.GetAsync(baseUrl +"get-all");

            //ASSERT
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);                      
            (await response.Content.ReadAsAsync<IEnumerable<CustomerDto>>()).Should().NotBeEmpty();
        }  
        
        [Fact]
        public async Task InsertCustomer_Should_Passed()
        {
            //ARRANGE
            await AuthenticateAsync();

            var customer = new AddCustomerCommand();
            customer.FirstName = "Guest";
            customer.LastName = "User";
            customer.Email = "user@gmail.com";
            customer.BirthDate = DateTime.Parse("1970-03-14");
            customer.Gender = Gender.NotSpecified;

            //ACT
            var response = await TestClient.PostAsJsonAsync(baseUrl + "insert", customer);

            //ASSERT
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CustomerDto>()).Should().NotBeNull();

        }

        [Fact]
        public async Task InsertCustomer_Should_Return_BadRequest()
        {
            //ARRANGE
            await AuthenticateAsync();

            var customer = new AddCustomerCommand();
            customer.FirstName = null;
            customer.LastName = string.Empty;
            customer.Email = "user@gmail.com";
            customer.BirthDate = DateTime.Parse("1970-03-14");
            customer.Gender = Gender.NotSpecified;

            //ACT
            var response = await TestClient.PostAsJsonAsync(baseUrl + "insert", customer);

            //ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);          
            Assert.Equal("Firstname and Lastname are required.", await response.Content.ReadAsStringAsync());

        }

        [Fact]
        public async Task GetCustomerById_Should_Return_Result_IfRecordExist()
        {
            ////ARRANGE
            await AuthenticateAsync();
            var customerId = CustomerDataFirstOrDefault().Id;

            var requestUrl = baseUrl + "get-by-id?id=customerId";
            requestUrl = requestUrl.Replace("customerId", customerId.ToString());

            //ACT
            var response = await TestClient.GetAsync(requestUrl);

            //ASSERT
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal(customerId, response.Content.ReadAsAsync<CustomerDto>().Result.Id);
        }

        [Fact]
        public async Task UpdateCustomer_LastRecord_Should_Passed()
        {
            //ARRANGE 
            await AuthenticateAsync();
            customer = CustomerDataLastOrDefault();          

            var newFirstName = "Updated Firstname";
            UpdateCustomerCommand param = new UpdateCustomerCommand();
            param.Customer = customer;
            param.Customer.FirstName = newFirstName;

            //ACT
            var response = await TestClient.PutAsJsonAsync(baseUrl + "update", param);

            //ASSERT
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal(newFirstName, response.Content.ReadAsAsync<CustomerDto>().Result.FirstName);
        }

        [Fact]
        public async Task UpdateCustomer_LastRecord_Should_Return_BadRequest()
        {
            //ARRANGE 
            await AuthenticateAsync();
            customer = CustomerDataFirstOrDefault();

            UpdateCustomerCommand param = new UpdateCustomerCommand();
            param.Customer = customer;
            param.Customer.FirstName = string.Empty;
            param.Customer.LastName = null;

            //ACT
            var response = await TestClient.PutAsJsonAsync(baseUrl + "update", param);

            //ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            Assert.Equal("Firstname and Lastname are required.", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task DeleteCustomer_LastRecord_Should_Delete_Customer_IfRecordExist()
        {
            //ARRANGE 
            await AuthenticateAsync();
            var customerId = CustomerDataLastOrDefault().Id;

            var requestUrl = baseUrl + "delete?id=customerId";
            requestUrl = requestUrl.Replace("customerId", customerId.ToString());

            //ACT
            var response = await TestClient.DeleteAsync(requestUrl);

            //ASSERT
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


    }
}
