using Moq;
using UniTestCaseApp.Services.Employee.Command;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UnitTest.App.Services.Employee.Command
{
    public class CreateEmployeeCommandHandlerTests
    {
        private MockRepository mockRepository;
        private Mock<IEmployeeRepository> mockEmployeeRepository;

        public CreateEmployeeCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockEmployeeRepository = this.mockRepository.Create<IEmployeeRepository>();
        }

        private CreateEmployeeCommandHandler CreateCreateEmployeeCommandHandler()
        {
            return new CreateEmployeeCommandHandler(this.mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsExpectedResult()
        {
            // Arrange
            var createEmployeeCommandHandler = this.CreateCreateEmployeeCommandHandler();

            // Create a valid command
            var request = new CreateEmployeeCommand(new UniTestCaseApp.Services.Employee.Model.Request.Employee
            {
                Name = "John Doe",
                Address = "123 Main St",
                Email = "johndoe@example.com",
                Department = "HR"
            });

            // Create a mock Employee to return from the repository
            var employeeFromRepo = new UniTestCaseApp.Services.Employee.Domain.Employee
            {
                Name = "John Doe",
                Address = "123 Main St",
                Email = "johndoe@example.com",
                Department = "HR"
            };

            // Setup the mock repository to return the mock employee
            mockEmployeeRepository.Setup(repo => repo.AddEmployee(It.IsAny<UniTestCaseApp.Services.Employee.Domain.Employee>()))
                .ReturnsAsync(employeeFromRepo);  // Use ReturnsAsync for async methods

            CancellationToken cancellationToken = default;

            // Act
            var result = await createEmployeeCommandHandler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeFromRepo.Name, result.Name);
            Assert.Equal(employeeFromRepo.Address, result.Address);
            Assert.Equal(employeeFromRepo.Email, result.Email);
            Assert.Equal(employeeFromRepo.Department, result.Department);

            // Verify that AddEmployee was called exactly once
            mockEmployeeRepository.Verify(repo => repo.AddEmployee(It.IsAny<UniTestCaseApp.Services.Employee.Domain.Employee>()), Times.Once);

            // Verify all mock expectations
            mockRepository.VerifyAll();
        }
    }
}
