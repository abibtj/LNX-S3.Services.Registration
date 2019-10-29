//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using OpenTracing;
//using S3.Common.Authentication;
//using S3.Common.Dispatchers;
//using S3.Common.Mvc;
//using S3.Common.RabbitMq;
//using S3.Common.Types;
//using S3.Services.Registration.Domain;
//using S3.Services.Registration.Dto;
//using S3.Services.Registration.Subjects.Commands;
//using S3.Services.Registration.Subjects.Queries;

//namespace S3.Services.Registration.Controllers
//{
//    //[JwtAuth(Roles = "superadmin")]
//    public class SubjectsController : BaseController
//    {
//        public SubjectsController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
//            : base(busPublisher, dispatcher, tracer ) { }

//        [HttpGet("browse")]
//        public async Task<IActionResult> GetAllAsync([FromQuery] BrowseSubjectsQuery query)
//            => Ok( await QueryAsync(query));

//        [HttpGet("get/{id:guid}")]
//        public async Task<IActionResult> GetByIdAsync(Guid id)
//            => Single(await QueryAsync(new GetSubjectQuery(id)));

//        [HttpPost("create")]
//        public async Task<IActionResult> Create(CreateSubjectCommand command)
//            => await SendAsync(command, resource: "subject");

//        [HttpPut("update")]
//        public async Task<IActionResult> Update(UpdateSubjectCommand command)
//            => await SendAsync(command, resourceId: command.Id, resource: "subject");

//        [HttpDelete("delete/{id:guid}")]
//        public async Task<IActionResult> Delete(Guid id)
//            => await SendAsync(new DeleteSubjectCommand(id), resourceId: id, resource: "subject");
//    }
//}
