using AutoMapper;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class MetaMappingProfile : Profile
    {
        public MetaMappingProfile()
        {
            CreateMap<Domain.Meta.Module, Module>();
            CreateMap<Domain.Meta.Entity, Entity>();
            CreateMap<Domain.Meta.Entity, EntityDetails>();
            CreateMap<Domain.Meta.Property, Property>();
        }
    }
}
