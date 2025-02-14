using MediatR;

namespace UniTestCaseApp.Services.Employee.Command
{
    public record CleanUpDatabaseCommand : IRequest<int>;
    
}
