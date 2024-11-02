namespace Provider
{
    public class StudentCreatedEvent
    {
        public required int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string Gender { get; set; }
        public Standard StandardId { get; set; }
    }
}
