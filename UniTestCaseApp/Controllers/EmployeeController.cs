using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniTestCaseApp.Services.Employee.Command;
using UniTestCaseApp.Services.Employee.Domain;
using UniTestCaseApp.Services.Employee.Queries;

namespace UniTestCaseApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;
        public EmployeeController(ISender sender) { 
            _sender = sender;
        }

        [HttpPost]
        public async Task<UniTestCaseApp.Services.Employee.Model.Request.Employee> CreateEmployee(CreateEmployeeCommand command)
        {
            var result = await _sender.Send(command);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var result = await _sender.Send(new GetEmployeeListCommand());
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _sender.Send(new GetEmployeeByIdQuery(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _sender.Send(new DeleteEmployeeCommand(id));

            if (result == 0)
            {
                // If no rows were affected, return a NotFound status
                return NotFound($"Employee with ID {id} not found.");
            }

            // If the employee was deleted, return a success status (HTTP 204 No Content)
            return NoContent(); // HTTP 204 No Content
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllEmployee()
        {
            var result = await _sender.Send(new CleanUpDatabaseCommand());

            if (result == 0)
            {
                // If no rows were affected, return a NotFound status
                return NotFound($"Employee with ID not found.");
            }

            // If the employee was deleted, return a success status (HTTP 204 No Content)
            return NoContent(); // HTTP 204 No Content
        }

    }
}
