namespace TodoBackend.Extensions
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
                            .WithOrigins("todofrontend://", "todofrontend://localhost", "https://localhost", "http://localhost:4200", "http://localhost:4300", "https://localhost:4200")
                            .AllowCredentials();
                    });
            });
        }
    }
}
