using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Mocks
{
    public class AppDbContextMock: AppDbContext
    {
        public AppDbContextMock() { }

        public AppDbContextMock(DbContextOptions options) : base(options)
        {
            IsMemoryDb = true;
        }

        public bool IsMemoryDb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!IsMemoryDb)
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PlanYourDayTest");
        }
    }
}