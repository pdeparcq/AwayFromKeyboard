using AwayFromKeyboard.Api.Domain.CodeGen;
using Microsoft.EntityFrameworkCore;

namespace AwayFromKeyboard.Api
{
    public class CodeGenDbContext : DbContext
    {
        public CodeGenDbContext(DbContextOptions<CodeGenDbContext> options) : base(options)
        {
        }

        public DbSet<Template> Templates { get; set; }
    }
}
