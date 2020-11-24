using System;
using IntegrationTests.Mocks;

namespace IntegrationTests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            using var context = new AppDbContextMock();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            using var context = new AppDbContextMock();
            context.Database.EnsureCreated();
        }
    }
}