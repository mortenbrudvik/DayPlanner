using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using AutoMapper;

namespace ApplicationCore.Mappers
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<TaskItem, TaskDto>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(source => source.Status.Value))
                .ReverseMap();
        }
    }
}