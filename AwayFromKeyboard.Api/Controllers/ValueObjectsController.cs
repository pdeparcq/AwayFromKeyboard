using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.ViewModels;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Property = AwayFromKeyboard.Api.Domain.Meta.Property;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValueObjectsController : Controller
    {
        private readonly MetaDbContext _metaDbContext;
        private readonly CodeGenDbContext _codeGenDbContext;
        private readonly IMapper _mapper;

        public ValueObjectsController(MetaDbContext metaDbContext, CodeGenDbContext codeGenDbContext, IMapper mapper)
        {
            _metaDbContext = metaDbContext;
            _codeGenDbContext = codeGenDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ValueObjectDetails> GetDetails(Guid id)
        {
            return _mapper.Map<ValueObjectDetails>(await GetValueObject(id));
        }

        [HttpGet("{id}/generate/{templateId}")]
        public async Task<GeneratedCode<ValueObject>> Generate(Guid id, Guid templateId)
        {
            // Get model
            var valueObject = await GetValueObject(id);

            // Get template
            var template = await _codeGenDbContext.Templates.FindAsync(templateId);

            // Generate and return code
            return new GeneratedCode<ValueObject>
            {
                Model = _mapper.Map<ValueObject>(valueObject),
                Template = _mapper.Map<Template>(template),
                Value = Handlebars.Compile(template.Value)(valueObject)
            };
        }

        [HttpPost]
        public async Task<ValueObject> Create([FromBody] CreateType model)
        {
            var value = await _metaDbContext.ValueObjects.AddAsync(new Domain.Meta.ValueObject()
            {
                Id = Guid.NewGuid(),
                ModuleId = model.ModuleId,
                Name = model.Name,
                Description = model.Description
            });
            await _metaDbContext.SaveChangesAsync();
            return _mapper.Map<ValueObject>(value.Entity);
        }

        [HttpPut]
        [Route("{id}/properties")]
        public async Task AddProperty(Guid id, [FromBody] AddProperty model)
        {
            var value = await _metaDbContext.ValueObjects.Include(e => e.Properties).SingleAsync(e => e.Id == id);

            value.Properties.Add(new Property
            {
                Name = model.Name,
                Description = model.Description,
                IsCollection = model.IsCollection,
                SystemType = model.SystemType,
                ValueTypeId = model.ValueTypeId
            });
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}/properties/{name}")]
        public async Task RemoveProperty(Guid id, string name)
        {
            var entity = await _metaDbContext.ValueObjects.Include(e => e.Properties).SingleAsync(e => e.Id == id);
            entity.Properties.Remove(entity.Properties.Single(p => p.Name == name));
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            _metaDbContext.ValueObjects.Remove(_metaDbContext.ValueObjects.Find(id));
            await _metaDbContext.SaveChangesAsync();
        }

        private async Task<Domain.Meta.ValueObject> GetValueObject(Guid id)
        {
            return await _metaDbContext.ValueObjects
                .Include(e => e.Properties).ThenInclude(p => p.ValueType)
                .SingleAsync(e => e.Id == id);
        }
    }
}
