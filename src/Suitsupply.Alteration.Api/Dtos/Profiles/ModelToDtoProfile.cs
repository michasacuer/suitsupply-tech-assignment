using AutoMapper;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Api.Dtos.Profiles;

public class ModelToDtoProfile : Profile
{
    public ModelToDtoProfile()
    {
        CreateMap<CustomerRequest, CustomerRequestDto>()
            .ForMember(x => x.Status, y => y.MapFrom(z => z.Status.ToString()));
    }
}