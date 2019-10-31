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
using S3.Services.Registration.Teachers.Commands;
using S3.Services.Registration.Teachers.Queries;

namespace S3.Services.Registration.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin")]
    public class TeachersController : BaseController
    {
        public TeachersController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
            : base(busPublisher, dispatcher, tracer) { }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync(string[]? include, Guid? schoolId, int page, int results, string orderBy, string sortOrder, bool returnSignedUpTeachers)
            => Ok(await QueryAsync(new BrowseTeachersQuery(include, schoolId, page, results, orderBy, sortOrder, returnSignedUpTeachers)));
        //public async Task<IActionResult> GetAllAsync([FromQuery] BrowseTeachersQuery query)
        //    => Ok( await QueryAsync(query));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, [FromQuery]string[]? include)
            => Single(await QueryAsync(new GetTeacherQuery(id, include)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTeacherCommand command)
            => await SendAsync(command, resource: "teacher");

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateTeacherCommand command)
            => await SendAsync(command, resourceId: command.Id, resource: "teacher");

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await SendAsync(new DeleteTeacherCommand(id), resourceId: id, resource: "teacher");
    }
}
