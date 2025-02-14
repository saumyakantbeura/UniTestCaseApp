using MediatR;

namespace UniTestCaseApp.Services.Employee.Queries
{
    public record GetEmployeeByIdQuery(int Id): IRequest<UniTestCaseApp.Services.Employee.Domain.Employee>;


}
