using server.Models;
using AutoMapper;

namespace server.MappingProfiles
{
    public class TodoMappings : Profile
    {
        public TodoMappings()
        {
            CreateMap<TodoEntity, TodoDto>().ReverseMap();
            CreateMap<TodoEntity, TodoCreateDto>().ReverseMap();
            CreateMap<TodoEntity, TodoUpdateDto>().ReverseMap();
        }
    }
}
