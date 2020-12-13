using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AwayFromKeyboard.Test
{
    [TestFixture]
    public class SandBox
    {
        [Test]
        public void CanUseMetaDbContext()
        {
            var options = new DbContextOptionsBuilder<MetaDbContext>().UseSqlite(CreateInMemoryDatabase()).Options;

            using (var ctx = new MetaDbContext(options))
            {
                var modules = ctx.Modules.Where(m => m.Name == "test");
                Assert.NotNull(modules);
            }
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
