namespace Provider
{
    public interface IStudentRepository
    {
        Student? GetById(int id);
        void Add(Student student);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students =
        [
            new()
            {
                Id = 10,
                FirstName = "James",
                LastName = "Hetfield",
                Address = "1234, 56th Street, San Francisco, USA",
                Gender = "male"
            }
        ];

        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Student student)
        {
            _students.Add(student);
        }
    }
}
