using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Interfaces
{
    public interface IApplicationDbContext
    {
       DbSet<TaskItem> Tasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}