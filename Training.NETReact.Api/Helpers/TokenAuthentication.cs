﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.NETReact.Api.Helpers
{
    public class TokenAuthentication : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;

            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;

            string token = string.Empty;
            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if (tokenManager.VerifyToken(token))
                    result = false;
            }

            if (!result)
            {
                context.ModelState.AddModelError("Unauthorized", "You are not authorized");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }

        }
    }
}
