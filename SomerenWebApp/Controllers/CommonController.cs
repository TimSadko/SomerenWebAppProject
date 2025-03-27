using SomerenWebApp.Repositories;

namespace SomerenWebApp.Controllers
{
	public static class CommonController
	{
		public static IActivityRepository _activity_rep;
		public static IStudentRepositorie _student_rep;
		public static ILecturerRepositorie _lecturer_rep;
		public static IRoomRepository _room_rep;
		public static ISupervisorReposiory _supervisor_rep;
		public static IParticipantsRepository _participant_rep;
	}
}
