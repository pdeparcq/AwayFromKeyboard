using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entity = AwayFromKeyboard.Api.ViewModels.Entity;
using Property = AwayFromKeyboard.Domain.Meta.Property;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class EntitiesController : Controller
    {
        private readonly MetaDbContext _metaDbContext;
        private readonly IMapper _mapper;

        public EntitiesController(MetaDbContext metaDbContext, IMapper mapper)
        {
            _metaDbContext = metaDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<EntityDetails> GetDetails(Guid id)
        {
            return _mapper.Map<EntityDetails>(await _metaDbContext.Entities
                .Include(e => e.Properties)
                .SingleAsync(e => e.Id == id));
        }

        [HttpPost]
        public async Task<Entity> Create([FromBody] CreateEntity model)
        {
            var entity = await _metaDbContext.Entities.AddAsync(new Domain.Meta.Entity()
            {
                Id = Guid.NewGuid(),
                ModuleId = model.ModuleId,
                Name = model.Name,
                Description = model.Description
            });
            await _metaDbContext.SaveChangesAsync();
            return _mapper.Map<Entity>(entity.Entity);
        }

        [HttpPut]
        [Route("{id}/properties")]
        public async Task AddProperty(Guid id, [FromBody] AddEntityProperty model)
        {
            var entity = await _metaDbContext.Entities.Include(e => e.Properties).SingleAsync(e => e.Id == id);

            entity.Properties.Add(new Property
            {
                Name = model.Name,
                Description = model.Description,
                IsCollection = model.IsCollection,
                IsIdentity = model.IsIdentity,
                SystemType = model.SystemType,
                ValueTypeId = model.ValueTypeId
            });
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}/properties/{name}")]
        public async Task RemoveProperty(Guid id, string name)
        {
            var entity = await _metaDbContext.Entities.Include(e => e.Properties).SingleAsync(e => e.Id == id);
            entity.Properties.Remove(entity.Properties.Single(p => p.Name == name));
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            _metaDbContext.Entities.Remove(_metaDbContext.Entities.Find(id));
            await _metaDbContext.SaveChangesAsync();
        }
    }
}
