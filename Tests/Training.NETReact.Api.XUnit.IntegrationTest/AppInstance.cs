using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Api.XUnit.IntegrationTest
{
    public class AppInstance
    {
        protected readonly HttpClient TestClient;
      
        protected AppInstance()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        #region not included
                        //services.RemoveAll(typeof(CustomerDataContext));
                        //services.AddDbContext<CustomerDataContext>(options => { options.UseInMemoryDatabase("CustomerDB"); });

                        //var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CustomerDataContext>));

                        //services.Remove(descriptor);

                        //services.AddDbContext<CustomerDataContext>(options =>
                        //{
                        //    options.UseInMemoryDatabase("CustomerDB");
                        //});

                        //var sp = services.BuildServiceProvider();

                        //using (var scope = sp.CreateScope())
                        //{
                        //    var scopedServices = scope.ServiceProvider;
                        //    var db = scopedServices.GetRequiredService<CustomerDataContext>();
                        //    db.Database.EnsureCreated();

                        //}
                        #endregion
                    });
                });

            TestClient = appFactory.CreateClient();
        }
       

        protected readonly string baseUrl = "/api/customer/";
        protected readonly string authenticationUrl = "api/Authenticate";

        #region for authentication
        private static IConfiguration _InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        private IConfiguration _config = _InitConfiguration();

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(authenticationUrl, new ClientInfo
            {
                SecretKey = _config.GetSection("ClientSecret")["Key"],
                ClientId = _config.GetSection("ClientSecret")["ClientId"]
            });

            var jsonContent = await response.Content.ReadAsStringAsync();
            dynamic tokenResponse = JObject.Parse(jsonContent);

            return tokenResponse.token;
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }       
        #endregion


        #region Customer Data List
        protected CustomerDto CustomerDataFirstOrDefault()
        {
            return CustomerList().FirstOrDefault();
        }

        protected CustomerDto CustomerDataLastOrDefault()
        {
            return CustomerList().LastOrDefault();
        }

        private IEnumerable<CustomerDto> CustomerList()
        {
            return TestClient.GetAsync(baseUrl + "get-all").Result.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result.ToList();
        }
        #endregion      
    }
}
