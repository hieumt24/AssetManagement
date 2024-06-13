﻿using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Domain.Entites;
using AutoMapper;

namespace AssetManagement.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<UserDto, UpdateUserRequestDto>().ReverseMap();
            CreateMap<AddUserRequestDto, User>().ReverseMap();
        }
    }
}