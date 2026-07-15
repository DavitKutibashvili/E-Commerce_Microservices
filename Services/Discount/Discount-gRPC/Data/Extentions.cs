using Microsoft.EntityFrameworkCore;

namespace Discount_gRPC.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            context.Database.Migrate();
            return app;
        }
    }
}
