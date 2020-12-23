using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Queries
{
    public class GetTasksQuery : IRequest<List<TaskDto>> { }

    public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTasksHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks =  await _dbContext.Tasks.ToListAsync(cancellationToken);

            return _mapper.Map<List<TaskDto>>(tasks);
        }
    }
}