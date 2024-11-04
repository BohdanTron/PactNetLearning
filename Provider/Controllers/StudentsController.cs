using MessageBroker;
using Microsoft.AspNetCore.Mvc;

namespace Provider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEventPublisher _eventPublisher;

        public StudentsController(
            IStudentRepository studentRepository,
            IEventPublisher eventPublisher)
        {
            _studentRepository = studentRepository;
            _eventPublisher = eventPublisher;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentRepository.GetById(id);

            return student is null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            var createdStudent = _studentRepository.Add(student);

            await _eventPublisher.Publish(new StudentCreatedEvent(createdStudent.Id), "student-created");

            return Ok();
        }
    }
}
