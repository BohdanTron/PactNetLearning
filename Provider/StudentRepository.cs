namespace Provider
{
    public interface IStudentRepository
    {
        Student? GetStudentById(int id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students =
        [
            new()
            {
                Id = 1,
                FirstName = "James",
                LastName = "Hetfield",
                Address = "1234, 56th Street, San Francisco, USA",
                Gender = "male"
            }
        ];

        public Student? GetStudentById(int id)
        {
            return _students.FirstOrDefault(x => x.Id == id);
        }
    }
}
