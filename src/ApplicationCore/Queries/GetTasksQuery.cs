using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Queries
{
    public class GetTasksQuery : IRequest<List<TaskDto>> { }

    public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetTasksHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var items =  await _dbContext.Tasks.ToListAsync(cancellationToken);

            return items.ToList().Select(x => new TaskDto()).ToList();
        }
    }
}