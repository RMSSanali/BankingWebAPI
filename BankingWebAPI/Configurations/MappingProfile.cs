using AutoMapper;
using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Define mappings between Entity and DTO
        CreateMap<Customer, CustomerDTO>();
    }
}
