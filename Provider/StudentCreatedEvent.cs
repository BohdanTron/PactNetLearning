namespace Provider
{
    public class StudentCreatedEvent
    {
        public required int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
    }
}
