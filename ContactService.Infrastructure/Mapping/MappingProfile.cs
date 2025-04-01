using AutoMapper;
using ContactService.Core.DTOs.ContactInfoDtos;
using ContactService.Core.DTOs.PersonDtos;
using ContactService.Core.Models;

namespace ContactService.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonDto, Person>();
            CreateMap<Person, PersonGetAllResponse>();
            CreateMap<AddContactInfoDto, ContactInformation>();
            CreateMap<Person, PersonDetailsResponse>()
      .ForMember(dest => dest.ContactInformations, opt => opt.MapFrom(src => src.ContactInformations));

            CreateMap<ContactInformation, ContactInformationDto>();

        }
    }
}
