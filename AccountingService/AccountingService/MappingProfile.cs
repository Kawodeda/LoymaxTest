using AccountingService.Dto;
using AccountingService.Dto.Requests;
using AccountingService.Models;
using AutoMapper;

namespace AccountingService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<RegisterClientRequest, Client>();
        }
    }
}