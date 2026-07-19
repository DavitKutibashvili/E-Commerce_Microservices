namespace Ordering_API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Register API services here
            // e.g., services.AddControllers();
            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure API services here
            // e.g., app.UseAuthentication();
            return app;
        }
    }
}
