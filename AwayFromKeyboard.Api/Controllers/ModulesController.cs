using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.ViewModels;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class ModulesController : Controller
    {
        private readonly MetaDbContext _metaDbContext;
        private readonly CodeGenDbContext _codeGenDbContext;
        private readonly IMapper _mapper;

        public ModulesController(MetaDbContext metaDbContext, CodeGenDbContext codeGenDbContext, IMapper mapper)
        {
            _metaDbContext = metaDbContext;
            _codeGenDbContext = codeGenDbContext;
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

        [HttpGet("{id}/generate/{templateId}")]
        public async Task<GeneratedCode<Module>> Generate(Guid id, Guid templateId)
        {
            // Get model
            var module = await _metaDbContext.Modules
                .Include(m => m.ValueObjects)
                .Include(m => m.Entities)
                .ThenInclude(e => e.DomainEvents).SingleAsync(m => m.Id == id);

            // Get template
            var template = await _codeGenDbContext.Templates.FindAsync(templateId);

            // Generate and return code
            return new GeneratedCode<Module>
            {
                Model = _mapper.Map<Module>(module),
                Template = _mapper.Map<Template>(template),
                Value = Handlebars.Compile(template.Value)(module)
            };
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
