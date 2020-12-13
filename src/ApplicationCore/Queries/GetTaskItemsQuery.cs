using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Queries
{
    public class GetTaskItemsQuery : IRequest<List<TaskItemDto>>
    {
        
    }

    public class GetTaskItemsHandler : IRequestHandler<GetTaskItemsQuery, List<TaskItemDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetTaskItemsHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TaskItemDto>> Handle(GetTaskItemsQuery request, CancellationToken cancellationToken)
        {
            var items =  await _dbContext.Tasks.ToListAsync(cancellationToken);

            return items.ToList().Select(x => new TaskItemDto()).ToList();
        }
    }
}