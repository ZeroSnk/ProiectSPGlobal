using Microsoft.EntityFrameworkCore;

namespace SandP.Models
{
    public class AgreementContext : DbContext
    {
        public AgreementContext(DbContextOptions<AgreementContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Agreement> Agreements { get; set; }
    }
}
