namespace Consumer
{
    public class StudentCreatedEvent
    {
        public required int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int StandardId { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
    }
}
