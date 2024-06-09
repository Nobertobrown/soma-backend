using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;

[Controller]
[Route("api/[controller]")]
public class StudentController : Controller
{
    private readonly MongoDBService _mongoDBService;
    public StudentController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Student>> Get()
    {
        return await _mongoDBService.GetStudents();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Student student)
    {
        await _mongoDBService.CreateStudent(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(string id, Student updatedStudent)
    {
        await _mongoDBService.UpdateStudent(id, updatedStudent);
        return Ok("Updated Successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteStudent(id);
        return Ok("Deleted Successfully.");
    }
}