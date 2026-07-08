using System;
using System.Threading.Tasks;
using AzureDevOpsDemo.Models;
using AzureDevOpsDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOpsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Position = dto.Position,
                DateHired = dto.DateHired
            };

            var created = await _repo.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _repo.GetAsync(id);
            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existing = await _repo.GetAsync(id);
            if (existing is null)
                return NotFound();

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.Position = dto.Position;
            existing.DateHired = dto.DateHired;

            var updated = await _repo.UpdateAsync(existing);
            if (updated is null)
                return NotFound();

            return NoContent();
        }
    }
}