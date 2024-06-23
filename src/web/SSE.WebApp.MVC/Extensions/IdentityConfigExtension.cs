using Microsoft.AspNetCore.Authentication.Cookies;

namespace SSE.WebApp.MVC.Extensions
{
    public static class IdentityConfigExtension
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/acesso-negado";

                });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app) 
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;

        }
    }
}
