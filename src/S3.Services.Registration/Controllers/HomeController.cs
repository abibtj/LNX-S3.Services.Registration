using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using S3.Common.Dispatchers;
using S3.Services.Registration.Schools.Commands;

namespace S3.Services.Registration.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Registration service running...");
    }
}
