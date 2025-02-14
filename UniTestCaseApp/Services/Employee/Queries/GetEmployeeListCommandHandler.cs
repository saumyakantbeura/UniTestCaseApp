using MediatR;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UniTestCaseApp.Services.Employee.Queries
{
    public class GetEmployeeListCommandHandler : IRequestHandler<GetEmployeeListCommand, List<UniTestCaseApp.Services.Employee.Domain.Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeListCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        async Task<List<Domain.Employee>> IRequestHandler<GetEmployeeListCommand, List<UniTestCaseApp.Services.Employee.Domain.Employee>>.Handle(GetEmployeeListCommand request, CancellationToken cancellationToken)
        {
           
            var resultset = await _employeeRepository.GetAllEmployees();
           
            return resultset;
        }
    }
}
