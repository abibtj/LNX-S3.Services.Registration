using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using S3.Services.Registration.Models;

namespace S3.Services.Registration.Controllers
{
    [Route("")]
    public class RegistrationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Registration service running");
    }
}
