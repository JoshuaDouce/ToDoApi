using AutoMapper;
using ToDoApi.Models;

namespace ToDoApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItemEntity, ToDoItem>();
        }
    }
}
