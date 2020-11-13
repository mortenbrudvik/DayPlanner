using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper.Contrib.Extensions;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
        private readonly IDbConnection _db;

        public TaskRepository(SqlConnection connection)
        {
            _db = connection;
        }

        public async Task<TaskItem> GetAsync(int id)
        {
            return await _db.GetAsync<TaskItem>(id);
        }

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
        {
            var tasks = await _db.GetAllAsync<TaskItem>();
            return tasks.ToList().AsReadOnly();
        }

        public async Task AddAsync(TaskItem task)
        {
            task.Created = DateTime.UtcNow;
            task.Id = await _db.InsertAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _db.DeleteAsync(new TaskItem {Id = id});
        }

        public async Task UpdateAsync(TaskItem task)
        {
            task.Updated = DateTime.UtcNow;
            await _db.UpdateAsync(task);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}