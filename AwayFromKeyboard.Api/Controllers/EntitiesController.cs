﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.InputModels;
using AwayFromKeyboard.Api.Services;
using AwayFromKeyboard.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantUml.Net;
using DomainEvent = AwayFromKeyboard.Api.Domain.Meta.DomainEvent;
using Entity = AwayFromKeyboard.Api.ViewModels.Entity;
using EntityRelation = AwayFromKeyboard.Api.Domain.Meta.EntityRelation;
using Property = AwayFromKeyboard.Api.Domain.Meta.Property;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class EntitiesController : Controller
    {
        private readonly MetaDbContext _metaDbContext;
        private readonly CodeGenDbContext _codeGenDbContext;
        private readonly IMapper _mapper;
        private readonly ICodeGenerator _generator;

        public EntitiesController(MetaDbContext metaDbContext, CodeGenDbContext codeGenDbContext, IMapper mapper, ICodeGenerator generator)
        {
            _metaDbContext = metaDbContext;
            _codeGenDbContext = codeGenDbContext;
            _mapper = mapper;
            _generator = generator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<EntityDetails> GetDetails(Guid id)
        {
            return _mapper.Map<EntityDetails>(await GetEntity(id));
        }

        [HttpGet("{id}/generate/{templateId}")]
        public async Task<GeneratedCode<Entity>> Generate(Guid id, Guid templateId)
        {
            // Get model
            var entity = await GetEntity(id);

            // Get template
            var template = await _codeGenDbContext.Templates.FindAsync(templateId);

            // Generate and return code
            return new GeneratedCode<Entity>
            {
                Model = _mapper.Map<Entity>(entity),
                Template = _mapper.Map<Template>(template),
                Value = _generator.GenerateCode(template, entity)
            };
        }

        [HttpGet("{id}/generate/{templateId}/uml")]
        public async Task<IActionResult> GenerateUmlDiagram(Guid id, Guid templateId)
        {
            // Get model
            var entity = await GetEntity(id);

            // Get template
            var template = await _codeGenDbContext.Templates.FindAsync(templateId);

            // Generate and return uml diagram
            var bytes = await new RendererFactory().CreateRenderer(new PlantUmlSettings
            {
                RenderingMode = RenderingMode.Remote
            }).RenderAsync(_generator.GenerateCode(template, entity), OutputFormat.Png);

            return File(bytes, "image/png");
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

        [HttpPut]
        [Route("{id}/relations")]
        public async Task AddRelation(Guid id, [FromBody] AddRelation model)
        {
            var entity = await _metaDbContext.Entities.Include(e => e.Relations).SingleAsync(e => e.Id == id);

            entity.Relations.Add(new EntityRelation
            {
                Name = model.Name,
                Description = model.Description,
                FromEntityId = id,
                ToEntityId = model.ToEntityId,
                Multiplicity = model.Multiplicity
            });
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}/relations/{name}")]
        public async Task RemoveRelation(Guid id, string name)
        {
            var entity = await _metaDbContext.Entities.Include(e => e.Relations).SingleAsync(e => e.Id == id);
            entity.Relations.Remove(entity.Relations.Single(p => p.Name == name));
            await _metaDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            _metaDbContext.Entities.Remove(_metaDbContext.Entities.Find(id));
            await _metaDbContext.SaveChangesAsync();
        }

        private async Task<Domain.Meta.Entity> GetEntity(Guid id)
        {
            return await _metaDbContext.Entities
                .Include(e => e.Module)
                .Include(e => e.Properties).ThenInclude(p => p.ValueType)
                .Include(e => e.Relations).ThenInclude(p => p.ToEntity)
                .Include(e => e.DomainEvents)
                .SingleAsync(e => e.Id == id);
        }
    }
}
