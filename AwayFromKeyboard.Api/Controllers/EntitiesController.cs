﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainEvent = AwayFromKeyboard.Domain.Meta.DomainEvent;
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
                .Include(e => e.Properties).ThenInclude(p => p.ValueType)
                .Include(e => e.DomainEvents)
                .SingleAsync(e => e.Id == id));
        }

        [HttpPost]
        public async Task<Entity> Create([FromBody] CreateType model)
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
        public async Task AddProperty(Guid id, [FromBody] AddProperty model)
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

        [HttpPut]
        [Route("{id}/domainEvents")]
        public async Task AddDomainEvent(Guid id, [FromBody] AddDomainEvent model)
        {
            var entity = await _metaDbContext.Entities.Include(e => e.DomainEvents).SingleAsync(e => e.Id == id);

            entity.DomainEvents.Add(new DomainEvent()
            {
                ModuleId = entity.ModuleId,
                AggregateRootId = entity.Id,
                Name = model.Name,
                Description = model.Description
            });
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
