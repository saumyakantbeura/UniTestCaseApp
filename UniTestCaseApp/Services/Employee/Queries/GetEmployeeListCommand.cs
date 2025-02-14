using MediatR;

namespace UniTestCaseApp.Services.Employee.Queries
{
    public record GetEmployeeListCommand :  IRequest<List<UniTestCaseApp.Services.Employee.Domain.Employee>>;
   
}
