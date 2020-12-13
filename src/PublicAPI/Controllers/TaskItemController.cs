using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Dtos;
using ApplicationCore.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PublicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tasks = await _mediator.Send(new GetTaskItemsQuery());

            return Ok(tasks);
        }
        
    }
}