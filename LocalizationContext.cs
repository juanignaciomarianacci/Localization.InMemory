using Microsoft.EntityFrameworkCore;

namespace Localizatio.InMemory
{
    public class LocalizationContext : DbContext
    {
        public LocalizationContext(DbContextOptions<LocalizationContext> options) : base(options)
        {

        }

        public DbSet<Localizations> Localizations { get; set; }
    }
}
