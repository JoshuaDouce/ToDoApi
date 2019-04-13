using AutoMapper;
using ToDoApi.Models;

namespace ToDoApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItemEntity, ToDoItem>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(
                    nameof(Controllers.ToDoItemController.GetToDoItem), new { id = src.Id})));
        }
    }
}
