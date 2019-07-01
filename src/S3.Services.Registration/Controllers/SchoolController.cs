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
    [Route("[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public SchoolController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public IActionResult Get() => Ok("Registration service running...");

        [HttpPost]
        public async Task<IActionResult> Post(CreateSchoolCommand command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}
