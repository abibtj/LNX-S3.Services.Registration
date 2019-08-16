﻿using System;
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
using S3.Services.Registration.Parents.Commands;
using S3.Services.Registration.Parents.Queries;

namespace S3.Services.Registration.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin")]
    public class ParentsController : BaseController
    {
        public ParentsController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
            : base(busPublisher, dispatcher, tracer )
        {
        }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BrowseParentsQuery query)
            => Ok( await QueryAsync(query));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Single(await QueryAsync(new GetParentQuery(id)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateParentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                return await SendAsync(command, resource: "parent");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateParentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await SendAsync(command,
              resourceId: command.Id, resource: "parent");
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) => await SendAsync(new DeleteParentCommand(id),
                resourceId: id, resource: "parent");
    }
}
