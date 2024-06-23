using Microsoft.EntityFrameworkCore;
using SSE.Catalogo.API.Model;
using SSE.Core.Data;

namespace SSE.Catalogo.API.Data
{
    public class CatalogoContext : DbContext, IUnitOfWorkc
    {

        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) 
        {
        }

        public DbSet<Product> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
