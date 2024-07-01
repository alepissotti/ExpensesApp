using AutoMapper;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;

namespace Expenses.Domain.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>()
            .ForMember(dest => dest.Id, src => src.MapFrom(prop => Hashid.Encode(prop.Id)));
        }
    }
}
