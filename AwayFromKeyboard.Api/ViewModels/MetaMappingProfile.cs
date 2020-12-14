using AutoMapper;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class MetaMappingProfile : Profile
    {
        public MetaMappingProfile()
        {
            CreateMap<Domain.Meta.Module, Module>();
        }
    }
}
