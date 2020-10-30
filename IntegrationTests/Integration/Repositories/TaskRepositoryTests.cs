using System;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using Infrastructure.Repositories;
using IntegrationTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Integration.Repositories
{
    [Collection("Database collection")]
    [Trait("Category", "Database")]
    public class TaskRepositoryTests : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private readonly ITestOutputHelper _output;
        private readonly TaskRepository _taskRepository;

        public TaskRepositoryTests(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
            _taskRepository = new TaskRepository(fixture.ConnectionString);
        }

        [Fact]
        public async void AddAsync_ShouldAssignIdToTask()
        {
            var task = CreateTask("Make some food");

            var insertedTask = await _taskRepository.AddAsync(task);

            _output.WriteLine(insertedTask.ToYaml());

            Assert.True(insertedTask.Id > 0);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmpty_WhenThereIsNoTasks()
        {
            var tasks = await _taskRepository.GetAll();
            
            Assert.Empty(tasks);
        }

        [Fact]
        public async void GetAll_ShouldNotBeEmpty_WhenThereIsTasks()
        {
            var task = CreateTask("Make some food");
            await _taskRepository.AddAsync(task);

            var tasks = await _taskRepository.GetAll();

            _output.WriteLine(tasks.ToYaml());

            Assert.NotEmpty(tasks);
        }

        [Fact]
        public async void Get_ShouldReturnTask_WhenTaskExist()
        {
            var expectedTask = CreateTask("Make some food");
            await _taskRepository.AddAsync(expectedTask);

            var actualTask = await _taskRepository.Get(expectedTask.Id);

            _output.WriteLine(actualTask.ToYaml());

            Assert.Equal(expectedTask.Id, actualTask.Id);
        }

        [Fact]
        public async void UpdateAsync_ShouldBeMarkedAsCompleted_WhenSettingCompletedIsSetToTrue()
        {
            var task = CreateTask("Make some food");
            await _taskRepository.AddAsync(task);

            task.IsCompleted = true;
            await _taskRepository.Update(task);

            var updatedTask = await _taskRepository.Get(task.Id);

            _output.WriteLine(updatedTask.ToYaml());

            Assert.True(updatedTask.IsCompleted);
        }

        [Fact]
        public async void Delete_ShouldDeleteTask_WhenTaskExist()
        {
            var task = CreateTask("Make some food");
            var insertedTask = await _taskRepository.AddAsync(task);

            await _taskRepository.Delete(insertedTask.Id);

            var tasks = await _taskRepository.GetAll();

            Assert.Empty(tasks);

        }

        private static TaskItem CreateTask(string name)
        {
            return new TaskItem()
            {
                Name = name
            };
        }

        public void Dispose()
        {
            _fixture.DeleteAllTasks();
        }
    }
}