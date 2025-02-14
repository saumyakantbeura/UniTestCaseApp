using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using UniTestCaseApp.Controllers;
using UniTestCaseApp.Services.Employee.Command;
using UniTestCaseApp.Services.Employee.Queries;
using Xunit;

namespace UnitTest.App.Controllers
{
    public class EmployeeControllerTests
    {
        // Test for CreateEmployee action
        [Fact]
        public async Task CreateEmployee_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var employeeController = new EmployeeController(mockSender.Object);

            // Create a valid CreateEmployeeCommand with a sample employee data
            var command = new CreateEmployeeCommand(new UniTestCaseApp.Services.Employee.Model.Request.Employee
            {
                Name = "John Doe",
                Address = "123 Main St",
                Email = "johndoe@example.com",
                Department = "HR"
            });

            // Define what the mock should return when the CreateEmployeeCommand is sent
            mockSender.Setup(sender => sender.Send(command, default))
                      .ReturnsAsync(new UniTestCaseApp.Services.Employee.Model.Request.Employee
                      {
                          Name = "John Doe",
                          Address = "123 Main St",
                          Email = "johndoe@example.com",
                          Department = "HR"
                      });

            // Act
            var result = await employeeController.CreateEmployee(command);

            // Assert: Check that the result is as expected (check properties for correctness)
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("123 Main St", result.Address);
            Assert.Equal("johndoe@example.com", result.Email);
            Assert.Equal("HR", result.Department);
        }

        // Test for GetEmployeeById action
        [Fact]
        public async Task GetEmployeeById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var employeeController = new EmployeeController(mockSender.Object);
            int employeeId = 1;

            // Define what the mock should return when the GetEmployeeByIdQuery is sent
            mockSender.Setup(sender => sender.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                      .ReturnsAsync(new UniTestCaseApp.Services.Employee.Domain.Employee
                      {
                          Id = 1,
                          Name = "John Doe",
                          Address = "123 Main St",
                          Email = "johndoe@example.com",
                          Department = "HR"
                      });

            // Act
            var result = await employeeController.GetEmployeeById(employeeId);

            // Assert: Check that the result is as expected (check response type and data)
            var okResult = Assert.IsType<OkObjectResult>(result);
            var employee = Assert.IsType<UniTestCaseApp.Services.Employee.Domain.Employee>(okResult.Value);

            Assert.Equal(employeeId, employee.Id);
            Assert.Equal("John Doe", employee.Name);
            Assert.Equal("123 Main St", employee.Address);
            Assert.Equal("johndoe@example.com", employee.Email);
            Assert.Equal("HR", employee.Department);
        }
    }
}
