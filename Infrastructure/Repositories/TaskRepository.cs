using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection _db;

        public TaskRepository(string connString)
        {
            _db = new SqlConnection(connString);
        }

        public async Task<TaskItem> Get(int id)
        {
            var tasks = await _db.QueryAsync<TaskItem>("SELECT * FROM TaskItems WHERE Id = @Id", new {Id = id});
            return tasks.SingleOrDefault();
        }

        public Task<IEnumerable<TaskItem>> GetAll()
        {
            return _db.QueryAsync<TaskItem>("SELECT * FROM TaskItems");
        }

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            task.Created = DateTime.UtcNow;
            const string sql = "INSERT INTO TaskItems (Name, Created) VALUES(@Name, @Created); " +
                               "SELECT CAST(SCOPE_IDENTITY() as int)";
            var result = _db.Query<int>(sql, task);
            task.Id = result.Single();
            return task;
        }

        public async Task Delete(int id)
        {
            await _db.ExecuteAsync("DELETE FROM TaskItems WHERE Id = @Id", new {Id = id});
        }

        public async Task<TaskItem> Update(TaskItem task)
        {
            task.Updated = DateTime.UtcNow;
            
            await _db.ExecuteAsync("UPDATE TaskItems " +
                            "SET Name = @Name, " +
                            "    Updated = @Updated, " +
                            "    IsCompleted = @IsCompleted " +
                            "WHERE Id = @Id", task);
            return task;
        }
    }
}