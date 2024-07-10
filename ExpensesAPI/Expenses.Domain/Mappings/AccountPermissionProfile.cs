using AutoMapper;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;

namespace Expenses.Domain.Mappings
{
    public class AccountPermissionProfile: Profile
    {
        public AccountPermissionProfile()
        {
            CreateMap<AccountPermission, AccountPermissionDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(prop => Hashid.Encode(prop.Id)));
        }
    }
}
