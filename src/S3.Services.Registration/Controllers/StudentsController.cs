using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
using S3.Services.Registration.Students.Commands;
using S3.Services.Registration.Students.Queries;

namespace S3.Services.Registration.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin")]
    public class StudentsController : BaseController
    {
        public StudentsController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
            : base(busPublisher, dispatcher, tracer ) { }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync([FromQuery]string[]? include, Guid? schoolId, Guid? classId, Guid? parentId, string? subject,
            int page, int results, string orderBy, string sortOrder)
            => Ok(await QueryAsync(new BrowseStudentsQuery(include, schoolId, classId, parentId, subject, page, results, orderBy, sortOrder)));
        //public async Task<IActionResult> GetAllAsync([FromQuery] BrowseStudentsQuery query)
        //    => Ok( await QueryAsync(query));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, [FromQuery]string[]? include)
            => Single(await QueryAsync(new GetStudentQuery(id, include)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStudentCommand command)
            => await SendAsync(command, resource: "student");

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        => await SendAsync(command, resourceId: command.Id, resource: "student");

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await SendAsync(new DeleteStudentCommand(id), resourceId: id, resource: "student");
    }
}
