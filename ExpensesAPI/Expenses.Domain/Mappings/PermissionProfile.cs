using AutoMapper;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;


namespace Expenses.Domain.Mappings
{
    public class PermissionProfile: Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(prop => Hashid.Encode(prop.Id)));
        }
    }
}
