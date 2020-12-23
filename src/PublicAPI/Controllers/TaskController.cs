using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Commands;
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
        public async Task<ActionResult<List<TaskDto>>> GetAll()
        {
            return await _mediator.Send(new GetTasksQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Get(int id)
        {
            var taskDto =  await _mediator.Send(new GetTaskQuery(id));
            
            if(taskDto==null)
                return NotFound();

            return taskDto;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTaskCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}