
using SSE.Catalogo.API.Data;
using SSE.Catalogo.API.Data.Repository;
using SSE.Catalogo.API.Model;

namespace SSE.Catalogo.API.Extentions
{
    public static class DependencyInjectionsExtentions
    { 
        public static void RegisteredServices (this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogoContext> ();
        }
    }
}
