using MediatR;

namespace UniTestCaseApp.Services.Employee.Command
{
   

    public record DeleteEmployeeCommand(int Id) : IRequest<int>;

}
