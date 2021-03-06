﻿using AutoMapper;
using AwayFromKeyboard.Api.ViewModels;

namespace AwayFromKeyboard.Api
{
    public class MetaMappingProfile : Profile
    {
        public MetaMappingProfile()
        {
            CreateMap<Domain.Meta.Module, Module>();
            CreateMap<Domain.Meta.Property, Property>();
            CreateMap<Domain.Meta.ValueObject, ValueObject>();
            CreateMap<Domain.Meta.ValueObject, ValueObjectDetails>();
            CreateMap<Domain.Meta.Entity, Entity>();
            CreateMap<Domain.Meta.Entity, EntityDetails>();
            CreateMap<Domain.Meta.EntityRelation, EntityRelation>();
            CreateMap<Domain.Meta.DomainEvent, DomainEvent>();
            CreateMap<Domain.Meta.DomainEvent, DomainEventDetails>();
        }
    }
}
