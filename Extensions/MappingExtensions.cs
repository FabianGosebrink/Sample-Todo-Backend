using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using server.MappingProfiles;

namespace server.Extensions
{
    public static class MappingExtensions
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TodoMappings));
        }
    }
}
