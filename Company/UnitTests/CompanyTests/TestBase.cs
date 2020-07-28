using Microsoft.EntityFrameworkCore;
using Persistence;
using System;

namespace UnitTests
{
    public class TestBase
    {
        public DataContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
           
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
           
            var dbContext = new DataContext(builder.Options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
