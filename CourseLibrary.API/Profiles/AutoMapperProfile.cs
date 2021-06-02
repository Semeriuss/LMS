using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<RegisterModel, User>();
        CreateMap<UpdateModel, User>();
        CreateMap<User, RegisterResponseDto>();
    }
}