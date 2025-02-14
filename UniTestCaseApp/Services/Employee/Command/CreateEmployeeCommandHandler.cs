using MediatR;
using System.Net;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UniTestCaseApp.Services.Employee.Command
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, UniTestCaseApp.Services.Employee.Model.Request.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<UniTestCaseApp.Services.Employee.Model.Request.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            UniTestCaseApp.Services.Employee.Model.Request.Employee result = new UniTestCaseApp.Services.Employee.Model.Request.Employee();
            Domain.Employee employee = new Domain.Employee()
            {
                Name = request.employee.Name,
                Address = request.employee.Address,
                Email = request.employee.Email,
                Department = request.employee.Department
            };

            var resultset = await _employeeRepository.AddEmployee(employee);
            if (resultset != null)
            {
                result.Name = resultset.Name;
                result.Address = resultset.Address;
                result.Email = resultset.Email;
                result.Department = resultset.Department;
            }
            return result;
        }
    }
}
