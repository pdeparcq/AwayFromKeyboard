using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwayFromKeyboard.Domain.Meta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class ModulesController : Controller
    {
        private readonly MetaDbContext _metaDbContext;

        public ModulesController(MetaDbContext metaDbContext)
        {
            _metaDbContext = metaDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Module>> Get()
        {
            return await _metaDbContext.Modules.Where(m => m.ParentModule == null).ToListAsync();
        }
    }
}
