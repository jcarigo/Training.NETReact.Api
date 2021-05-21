using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Training.NETReact.Api.Helpers
{
    public class JwtToken
    {
        private readonly IConfiguration Configuration;
        public static SymmetricSecurityKey SIGNING_KEY;

        public JwtToken(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string GenerateToken()
        {
            //securitykey legnth should be >256
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("ClientSecret")["Key"]));
            var credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Sub, "ReatApp"),
                //new Claim(JwtRegisteredClaimNames.Email, "test@gmail.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Configuration.GetSection("ClientSecret")["Issuer"],
                audience: Configuration.GetSection("ClientSecret")["Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: credentials);

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return generatedToken;

        }


        #region other iplementation
        public string GenerateJwtToken()
        {
            //securitykey legnth should be >256
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("ClientSecret")["Key"]));
            var credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            //create token
            var header = new JwtHeader(credentials);
            DateTime Expiry = DateTime.Now.AddHours(1);
            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub", "ReactApp" },
                {"name", "test"},
                {"email", "test@gmail.com" },
                {"exp", ts },
                {"iss", Configuration.GetSection("ClientSecret")["Issuer"]},
                {"aud", Configuration.GetSection("ClientSecret")["Audience"] }
            };

            var securityToken = new JwtSecurityToken(header, new JwtPayload());

            var handler = new JwtSecurityTokenHandler();

            var generatedToken = handler.WriteToken(securityToken);

            return generatedToken;
        }
        #endregion


    }
}
