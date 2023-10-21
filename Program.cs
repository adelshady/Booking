using Booking.Application.InterfaceForRepos;
using Booking.Application.Service.Implemntation;
using Booking.Infrastructure.Data;

using Booking.Application.Service.Interface;
using Booking.Infrastructure.Repository;


using Booking.Appliaction.Service.Interface;
using Booking.Appliaction.Service.Implemntation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Booking.Domain.Entity;

namespace Booking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<BookingDbContext>(options =>
            
                options.UseSqlServer(builder.Configuration.GetConnectionString("db"))
            );


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookingDbContext>();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //builder.Services.AddScoped<IVillaRepo, VillaRepo>();

            builder.Services.AddScoped<IVillaService, VillaSerivce>();
            builder.Services.AddScoped<IAmenitySerivce, AmenitySerivce>();
            builder.Services.AddScoped<IVillaNumberService, VillaNumberSerivce>();
            builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();

           
            var app = builder.Build();
            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbInitializer = scope.ServiceProvider.GetRequiredService<BookingDbContext>;
            //}
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


           // app.UseAuthentication();
            app.UseAuthorization();
          

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            
        }
    }
}

