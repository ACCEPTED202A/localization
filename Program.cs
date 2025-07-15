using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace localization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            #region "Language"
            //========== localization
            //????? ???? localization
            builder.Services.AddLocalization(option=> option.ResourcesPath="Resources");
            builder.Services.Configure<RequestLocalizationOptions>(option => {
                //
                // ?? ???? configration 
                //??????? ???? ?? en and ar

                var supportedCulter = new[] {
                    new CultureInfo("en"),
                    new CultureInfo("ar")
                };
                // defult lang
                option.DefaultRequestCulture = new RequestCulture("en");
                option.SupportedCultures = supportedCulter;
                option.SupportedUICultures = supportedCulter;
            });
            #endregion 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            #region "Mw_language"
            // =========== localization
            //create varable and inj to localization
            var localizationOption = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOption!.Value);
            // ===========
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            #region new_url
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoint.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            #endregion

            app.Run();
        }
    }
}
