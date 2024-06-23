
namespace SSE.Identidade.API.Extensions
{
    public static class AppConfigExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddLogging();

            return services;
        }

        public static IApplicationBuilder UseAppConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
