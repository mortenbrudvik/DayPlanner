using System;
using IntegrationTests.Mocks;

namespace IntegrationTests.Fixtures
{
    public class EFFixture : IDisposable
    {
        public EFFixture()
        {
            using var context = new AppDbContextMock();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            using var context = new AppDbContextMock();
            context.Database.EnsureDeleted();
        }
    }
}