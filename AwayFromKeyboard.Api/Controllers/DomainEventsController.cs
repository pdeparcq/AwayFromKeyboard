﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Property = AwayFromKeyboard.Api.Domain.Meta.Property;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class DomainEventsController : Controller
    {
        private readonly MetaDbContext _metaDbContext;
        private readonly IMapper _mapper;

        public DomainEventsController(MetaDbContext metaDbContext, IMapper mapper)
        {
            _metaDbContext = metaDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DomainEventDetails> GetDetails(Guid id)
        {
            return _mapper.Map<DomainEventDetails>(await _metaDbContext.DomainEvents
                .Include(e => e.Properties).ThenInclude(p => p.ValueType)
                .SingleAsync(e => e.Id == id));
        }

        [HttpPut]
        [Route("{id}/properties")]
        public async Task AddProperty(Guid id, [FromBody] AddProperty model)
        {
            var value = await _metaDbContext.DomainEvents.Include(e => e.Properties).SingleAsync(e => e.Id == id);

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
            var entity = await _metaDbContext.DomainEvents.Include(e => e.Properties).SingleAsync(e => e.Id == id);
            entity.Properties.Remove(entity.Properties.Single(p => p.Name == name));
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            _metaDbContext.DomainEvents.Remove(_metaDbContext.DomainEvents.Find(id));
            await _metaDbContext.SaveChangesAsync();
        }
    }
}