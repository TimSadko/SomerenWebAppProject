namespace SomerenWebApp.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int StudentId { get;  set; }
        public int ActivityId { get;  set; }

        public Participant() { }
        
        public Participant(int id, int studentId, int activityId)
        {
            
            Id= id;
            StudentId = studentId;
            ActivityId = activityId;
        }
    }
}
