using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using IntegrationTests.Fixtures;
using IntegrationTests.Mocks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Infrastructure.Repositories
{
    [Collection("Database collection")]
    [Trait("Category", "Database")]
    public class AppDbContextTests
    {
        private readonly EFFixture _fixture;
        private readonly ITestOutputHelper _output;

        public AppDbContextTests(EFFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task AddAsync_ShouldSetCreatedDate()
        {
            var task = CreateTask("Make some food");

            await using var context = new AppDbContextMock();
            await context.AddAsync(task);
            await context.SaveChangesAsync();
            _output.WriteLine(task.ToYaml());

            task.Created.ShouldNotBe(DateTime.MinValue);
        }
        
        [Fact]
        public async Task UpdateAsync_ShouldSetUpdatedDate()
        {
            var task = CreateTask("Make some food");

            await using var context = new AppDbContextMock();
            await context.AddAsync(task);
            await context.SaveChangesAsync();

            task.Title += " now";
            await context.SaveChangesAsync();
            _output.WriteLine(task.ToYaml());

            task.Updated.ShouldNotBeNull();
        }

        private static TaskItem CreateTask(string name)
        {
            return new TaskItem()
            {
                Title = name
            };
        }
    }
}