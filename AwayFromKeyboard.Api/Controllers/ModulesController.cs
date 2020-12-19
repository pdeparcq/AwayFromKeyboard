using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class ModulesController : Controller
    {
        private readonly MetaDbContext _metaDbContext;
        private readonly IMapper _mapper;

        public ModulesController(MetaDbContext metaDbContext, IMapper mapper)
        {
            _metaDbContext = metaDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Module>> Get()
        {
            return _mapper.Map<IEnumerable<Module>>(await _metaDbContext.Modules
                .Include(m => m.ValueObjects)
                .Include(m => m.Entities)
                .ThenInclude(e => e.DomainEvents)
                .Where(m => m.ParentModule == null)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<Module> Create([FromBody] CreateModule model)
        {
            var module = await _metaDbContext.Modules.AddAsync(new Domain.Meta.Module
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                ParentModuleId = model.ParentModuleId
            });
            _metaDbContext.SaveChanges();
            return _mapper.Map<Module>(module.Entity);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            _metaDbContext.Modules.Remove(_metaDbContext.Modules.Find(id));
            await _metaDbContext.SaveChangesAsync();
        }
    }
}
