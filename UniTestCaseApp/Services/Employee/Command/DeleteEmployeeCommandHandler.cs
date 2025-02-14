using MediatR;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UniTestCaseApp.Services.Employee.Command
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        async Task<int> IRequestHandler<DeleteEmployeeCommand, int>.Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var resultset = await _employeeRepository.DeleteEmployee(request.Id);

            return resultset;
        }
    }
}
