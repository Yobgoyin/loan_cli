using applicant_api.Model.Data.Class;
using Microsoft.EntityFrameworkCore;

namespace applicant_api.Context
{
    public class ApplicationQuoteContext : DbContext
    {
        public ApplicationQuoteContext(DbContextOptions<ApplicationQuoteContext> options)
            : base(options)
        {
        }
        public DbSet<Applicant> Applicant { get; set; } = null!;
        public DbSet<Quote> Quote { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
    }
}
