using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.NETReact.Api.Helpers;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private JwtToken JwtToken;

        public AuthenticateController(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.JwtToken = new JwtToken(configuration);
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] ClientInfo client)
        {
            IActionResult response = Unauthorized();
            if (client.SecretKey == Configuration.GetSection("ClientSecret")["Key"] && client.ClientId == Configuration.GetSection("ClientSecret")["ClientId"])
            {
                var generatedToken = JwtToken.GenerateToken();
                response = Ok(new { token = generatedToken });
            }

            return response;
        }
    }
}
