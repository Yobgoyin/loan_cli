using Microsoft.EntityFrameworkCore;

namespace applicant_api.Context
{
    public static class DBHandler
    {
        public static void InitMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceDb = serviceScope.ServiceProvider
                                    .GetService<ApplicationQuoteContext>();

                serviceDb.Database.Migrate();
            }
        }
    }
}
