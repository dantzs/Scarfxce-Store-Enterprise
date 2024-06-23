using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSE.Catalogo.API.Data;

namespace SSE.Catalogo.API.Extentions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<CatalogoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConncetion"));
            });

            services.AddControllers();

            services.AddCors(options =>
            {
            options.AddPolicy("Total",
                builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            return services;


        }
        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Total");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseAuthorization();

            return app;
        }
    }
}
