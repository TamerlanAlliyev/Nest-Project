using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nest.Data;
using Nest.Models;
using Nest.ViewComponents;

namespace Nest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<NestContext>(cfg =>
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"))
            );



            builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
            {

                option.User.RequireUniqueEmail = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = true;
                        
            }).AddEntityFrameworkStores<NestContext>().AddDefaultTokenProviders();




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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}