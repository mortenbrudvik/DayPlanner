using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Commands
{
    public class CreateTaskCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateTaskCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem {Title = request.Title};

            await _appDbContext.Tasks.AddAsync(task, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}