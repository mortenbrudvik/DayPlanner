using ApplicationCore.Entities;
using ApplicationCore.Models;
using AutoMapper;

namespace ApplicationCore.Mappers
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<TaskItem, TaskDto>();
            CreateMap<TaskDto, TaskItem>();
        }
    }
}