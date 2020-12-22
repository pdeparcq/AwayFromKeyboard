using AutoMapper;
using AwayFromKeyboard.Api.ViewModels;

namespace AwayFromKeyboard.Api
{
    public class CodeGenMappingProfile : Profile
    {
        public CodeGenMappingProfile()
        {
            CreateMap<Domain.CodeGen.Template, Template>();
        }
    }
}
