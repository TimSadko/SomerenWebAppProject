using SomerenWebApp.Repositories;

namespace SomerenWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Connect all repositories objects
			builder.Services.AddSingleton<IStudentRepositorie, DBStudentRepositorie>();
			builder.Services.AddSingleton<ILecturerRepositorie, DBLecturerRepositorie>();
      builder.Services.AddSingleton<IRoomRepository, DBRoomRepository>();   
      builder.Services.AddSingleton<IActivityRepository, DBActivityRepository>();

			// Add services to the container.          
			builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
