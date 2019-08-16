using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using S3.Common.Authentication;
using S3.Common.Dispatchers;
using S3.Common.Mvc;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Schools.Commands;
using S3.Services.Registration.Schools.Queries;

namespace S3.Services.Registration.Controllers
{
    //[JwtAuth(Roles = "superadmin")]
    public class SchoolsController : BaseController
    {
        public SchoolsController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
            : base(busPublisher, dispatcher, tracer )
        {
        }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BrowseSchoolsQuery query)
            => Ok( await QueryAsync(query));
        //=> Collection(await QueryAsync(query));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Single(await QueryAsync(new GetSchoolQuery(id)));

        [HttpGet("get/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
           => Single(await QueryAsync(new GetSchoolQuery(name)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateSchoolCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                return await SendAsync(command, resource: "school");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateSchoolCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await SendAsync(command,
              resourceId: command.Id, resource: "school");
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) => await SendAsync(new DeleteSchoolCommand(id),
                resourceId: id, resource: "school");

        //[HttpDelete("delete")]
        //public async Task<IActionResult> Delete(DeleteSchoolCommand command) => await SendAsync(command,
        //       resourceId: command.Id, resource: "school");
    }
}
