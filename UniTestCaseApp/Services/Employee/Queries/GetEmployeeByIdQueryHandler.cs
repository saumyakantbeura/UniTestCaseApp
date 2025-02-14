using MediatR;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UniTestCaseApp.Services.Employee.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, UniTestCaseApp.Services.Employee.Domain.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            UniTestCaseApp.Services.Employee.Model.Request.Employee result = new UniTestCaseApp.Services.Employee.Model.Request.Employee();
            var resultset = await _employeeRepository.GetEmployee(request.Id);
            if (resultset != null)
            {
                result.Name = resultset.Name;
                result.Address = resultset.Address;
                result.Email = resultset.Email;
                result.Department = resultset.Department;
            }
            return resultset;
        }
    }
}
