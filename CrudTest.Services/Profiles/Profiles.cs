using AutoMapper;
using CrudTest.Domain.Dtos;
using CrudTest.Domain.Models;

namespace CrudTest.Services.Profiles;

public class Profiles : Profile
{
    public Profiles() => CreateMap<User, UserDto>().ReverseMap();
}
