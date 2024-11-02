using Microsoft.AspNetCore.Mvc;

namespace Provider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentRepository.GetById(id);

            return student is null ? NotFound() : Ok(student);
        }
    }
}
