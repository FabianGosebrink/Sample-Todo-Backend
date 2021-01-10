using Microsoft.Extensions.DependencyInjection;

namespace server.Extensions
{
    public static class CorsExtension
    {
        public static void AddCustomCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:4200")
                            .AllowCredentials();
                    });
            });
        }
    }
}
