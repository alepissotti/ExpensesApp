using AutoMapper;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;

namespace Expenses.Domain.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(prop => Hashid.Encode(prop.Id)));
        }
    }
}
