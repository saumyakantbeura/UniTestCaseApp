using MediatR;

namespace UniTestCaseApp.Services.Employee.Command
{
    public record CreateEmployeeCommand(UniTestCaseApp.Services.Employee.Model.Request.Employee employee) : IRequest<UniTestCaseApp.Services.Employee.Model.Request.Employee>;
    
}
