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
using S3.Services.Registration.ScoresEntryTasks.Commands;
using S3.Services.Registration.ScoresEntryTasks.Queries;

namespace S3.Services.Registration.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin")]
    public class ScoresEntryTasksController : BaseController
    {
        public ScoresEntryTasksController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
            : base(busPublisher, dispatcher, tracer ) { }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync(string[]? include, Guid? schoolId, Guid? teacherId, int page, int results, string orderBy, string sortOrder)
            => Ok(await QueryAsync(new BrowseScoresEntryTasksQuery(include, schoolId, teacherId, page, results, orderBy, sortOrder)));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, [FromQuery]string[]? include)
            => Single(await QueryAsync(new GetScoresEntryTaskQuery(id, include)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateScoresEntryTaskCommand command)
            => await SendAsync(command, resource: "scoresEntryTask");

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateScoresEntryTaskCommand command)
        => await SendAsync(command, resourceId: command.Id, resource: "scoresEntryTask");

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await SendAsync(new DeleteScoresEntryTaskCommand(id), resourceId: id, resource: "scoresEntryTask");
    }
}
