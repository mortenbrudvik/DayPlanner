using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Queries
{
    public class GetTaskQuery : IRequest<TaskDto>
    {
        public int Id { get; }

        public GetTaskQuery(int id)
        {
            Id = id;
        }
    }

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<TaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Tasks.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            return _mapper.Map<TaskDto>(item);
        }
    }
}