using Moq;
using UniTestCaseApp.Services.Employee.Command;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UnitTest.App.Services.Employee.Command
{
    public class CreateEmployeeCommandTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandTests()
        {
            // Initialize the mock repository
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            // Initialize the handler with the mocked repository
            _handler = new CreateEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        private CreateEmployeeCommand CreateCreateEmployeeCommand()
        {
            // Return a valid CreateEmployeeCommand with sample data
            var employeeRequest = new UniTestCaseApp.Services.Employee.Model.Request.Employee
            {
                Name = "John Doe",
                Address = "123 Main St",
                Email = "johndoe@example.com",
                Department = "HR"
            };
            return new CreateEmployeeCommand(employeeRequest);
        }

        [Fact]
        public async Task Handle_CreateEmployeeCommand_ReturnsExpectedResult()
        {
            // Arrange
            var command = CreateCreateEmployeeCommand();

            // Prepare mock to simulate adding employee and returning the same employee
            var employee = new UniTestCaseApp.Services.Employee.Domain.Employee
            {
                Name = "John Doe",
                Address = "123 Main St",
                Email = "johndoe@example.com",
                Department = "HR"
            };

            _mockEmployeeRepository.Setup(repo => repo.AddEmployee(It.IsAny<UniTestCaseApp.Services.Employee.Domain.Employee>()))
                .ReturnsAsync(employee);  // Simulate the AddEmployee method returning the employee

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Name, result.Name);
            Assert.Equal(employee.Address, result.Address);
            Assert.Equal(employee.Email, result.Email);
            Assert.Equal(employee.Department, result.Department);

            // Verify that AddEmployee was called exactly once
            _mockEmployeeRepository.Verify(repo => repo.AddEmployee(It.IsAny<UniTestCaseApp.Services.Employee.Domain.Employee>()), Times.Once);
        }
    }
}
