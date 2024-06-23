namespace SSE.WebApp.MVC.Extensions
{
    public static class WebAppConfigExtension
    {
        public static void AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();
        }


        public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseIdentityConfiguration();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseIdentityConfiguration();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }

    }
}
