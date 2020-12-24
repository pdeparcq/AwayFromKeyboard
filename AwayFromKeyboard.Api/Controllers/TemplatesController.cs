using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwayFromKeyboard.Api.Domain.CodeGen;
using AwayFromKeyboard.Api.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template = AwayFromKeyboard.Api.ViewModels.Template;

namespace AwayFromKeyboard.Api.Controllers
{
    [Route("api/[controller]")]
    public class TemplatesController : Controller
    {
        private readonly CodeGenDbContext _codeGenDbContext;
        private readonly IMapper _mapper;

        public TemplatesController(CodeGenDbContext codeGenDbContext, IMapper mapper)
        {
            _codeGenDbContext = codeGenDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Template>> GetByMetaType([FromQuery] MetaType metaType)
        {
            return _mapper.Map<IEnumerable<Template>>(await _codeGenDbContext.Templates
                .Where(t => t.MetaType == metaType)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<Template> GetById(Guid id)
        {
            return _mapper.Map<Template>(await _codeGenDbContext.Templates.FindAsync(id));
        }

        [HttpPost]
        public async Task<Template> Create([FromBody] CreateTemplate model)
        {
            var template = await _codeGenDbContext.Templates.AddAsync(new Domain.CodeGen.Template()
            {
                Id = Guid.NewGuid(),
                MetaType = model.MetaType,
                Name = model.Name,
                Value = model.Value
            });
            _codeGenDbContext.SaveChanges();
            return _mapper.Map<Template>(template.Entity);
        }

        [HttpPut("{id}")]
        public async Task<Template> Update(Guid id, [FromBody] UpdateTemplate model)
        {
            var template = await _codeGenDbContext.Templates.FindAsync(id);
            template.Value = model.Value;
            _codeGenDbContext.SaveChanges();
            return _mapper.Map<Template>(template);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            _codeGenDbContext.Templates.Remove(_codeGenDbContext.Templates.Find(id));
            await _codeGenDbContext.SaveChangesAsync();
        }
    }
}
