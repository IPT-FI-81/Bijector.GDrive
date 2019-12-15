using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bijector.GDrive.Models;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Repositories;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bijector.GDrive.Controllers
{
    [Route("GDriveAuth")]
    public class GoogleDriveAuthController : Controller
    {        
        private readonly IGoogleAuthService authService;

        public GoogleDriveAuthController(IGoogleAuthService authService)
        {
            this.authService = authService;            
        }
        
        [Authorize]
        [HttpGet("Auth")]
        public IActionResult Authenticate()
        {            
            return Redirect(authService.GetAuthorizationRequestUrl());            
        }

        [Authorize]
        [HttpGet("GetAuthToken")]
        public async Task<IActionResult> GetAuthenticationToken([FromQuery] string code)
        {
            if(string.IsNullOrEmpty(code))
            {
                return Unauthorized();
            }
            
            var accountId = Guid.Parse(User.Identity.Name);
            await authService.StoreTokenFromCode(accountId, code);
            
            return Redirect("/GDrive/GetAll");
        }
    }
}