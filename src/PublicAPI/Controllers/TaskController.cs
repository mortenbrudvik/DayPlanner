using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PublicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> Get()
        {
            return await _mediator.Send(new GetTasksQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Get(int id)
        {
            var task =  await _mediator.Send(new GetTaskQuery(id));
            
            if(task==null)
                return NotFound();

            return task;
        }
    }
}