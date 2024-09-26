using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TodoBackend.MappingProfiles;

namespace TodoBackend.Extensions
{
    public static class MappingExtensions
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TodoMappings));
        }
    }
}
