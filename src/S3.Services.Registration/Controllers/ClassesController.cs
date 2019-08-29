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
using S3.Services.Registration.Classes.Commands;
using S3.Services.Registration.Classes.Queries;

namespace S3.Services.Registration.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin")]
    public class ClassesController : BaseController
    {
        public ClassesController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
            : base(busPublisher, dispatcher, tracer ) { }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BrowseClassesQuery query)
            => Ok( await QueryAsync(query));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Single(await QueryAsync(new GetClassQuery(id)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateClassCommand command)
            => await SendAsync(command, resource: "class");

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateClassCommand command)
            => await SendAsync(command, resourceId: command.Id, resource: "class");

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) 
            => await SendAsync(new DeleteClassCommand(id), resourceId: id, resource: "class");
    }
}
