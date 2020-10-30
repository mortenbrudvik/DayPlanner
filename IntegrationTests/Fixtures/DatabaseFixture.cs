using System;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Fixtures
{
    public class DatabaseFixture :IDisposable
    {
        private static IConfigurationRoot config;

        public string ConnectionString => config.GetConnectionString("DefaultConnection");

        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config = builder.Build();
        }

        public void DeleteAllTasks()
        {
            var db = new SqlConnection(ConnectionString);
            db.Execute("DELETE FROM TaskItems");
        }

        public void Dispose()
        {
            DeleteAllTasks();
        }
    }
}