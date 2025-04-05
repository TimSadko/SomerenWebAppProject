using SomerenWebApp.Controllers;
using SomerenWebApp.Repositories;
using SomerenWebApp.Repositories;

namespace SomerenWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            DefaultConfiguration def = new DefaultConfiguration(builder.Configuration.GetConnectionString("MessengerDatabase"));

            // Connect all repositories objects
            var _student_rep = new DBStudentRepositorie(def);
			builder.Services.AddSingleton<IStudentRepositorie>(_student_rep);
            CommonController._student_rep = _student_rep;

			var _lecturer_rep = new DBLecturerRepositorie(def);
			builder.Services.AddSingleton<ILecturerRepositorie>(_lecturer_rep);
            CommonController._lecturer_rep = _lecturer_rep;

			var _room_rep = new DBRoomRepository(def);
			builder.Services.AddSingleton<IRoomRepository>(_room_rep);
            CommonController._room_rep = _room_rep;

			var _activity_rep = new DBActivityRepository(def);
			builder.Services.AddSingleton<IActivityRepository>(_activity_rep);
            CommonController._activity_rep = _activity_rep;

            var _supervisor_rep = new DBSupervisorReposiory(def);
            builder.Services.AddSingleton<ISupervisorReposiory>(_supervisor_rep);
            CommonController._supervisor_rep = _supervisor_rep;

            var _participants_rep = new DBParticipantRepository(def);
            builder.Services.AddSingleton<IParticipantsRepository>(_participants_rep);
            CommonController._participants_rep = _participants_rep;

            var _drink_rep = new DBDrinksRepository(def);
            builder.Services.AddSingleton<IDrinksRepository>(_drink_rep);
            CommonController._drink_rep = _drink_rep;

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
