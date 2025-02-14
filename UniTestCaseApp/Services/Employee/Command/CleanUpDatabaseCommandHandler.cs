using MediatR;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UniTestCaseApp.Services.Employee.Command
{
    public class CleanUpDatabaseCommandHandler : IRequestHandler<CleanUpDatabaseCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CleanUpDatabaseCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        async Task<int> IRequestHandler<CleanUpDatabaseCommand, int>.Handle(CleanUpDatabaseCommand request, CancellationToken cancellationToken)
        {
            var resultset = await _employeeRepository.DeleteAllEmployees();
            return resultset;
        }
    }
}
