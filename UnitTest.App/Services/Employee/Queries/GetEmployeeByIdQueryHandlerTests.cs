using Moq;
using UniTestCaseApp.Services.Employee.Queries;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;

namespace UnitTest.App.Services.Employee.Queries
{
    public class GetEmployeeByIdQueryHandlerTests
    {
        private MockRepository mockRepository;
        private Mock<IEmployeeRepository> mockEmployeeRepository;

        public GetEmployeeByIdQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockEmployeeRepository = this.mockRepository.Create<IEmployeeRepository>();
        }

        private GetEmployeeByIdQueryHandler CreateGetEmployeeByIdQueryHandler()
        {
            return new GetEmployeeByIdQueryHandler(this.mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsExpectedEmployee()
        {
            // Arrange
            var getEmployeeByIdQueryHandler = this.CreateGetEmployeeByIdQueryHandler();

            // Create a valid query with a test employee ID
            var employeeId = 1;
            var request = new GetEmployeeByIdQuery(employeeId);

            // Create a mock Employee to return from the repository
            var employeeFromRepo = new UniTestCaseApp.Services.Employee.Domain.Employee
            {
                Id = employeeId,
                Name = "John Doe",
                Address = "123 Main St",
                Email = "johndoe@example.com",
                Department = "HR"
            };

            // Setup the mock repository to return the mock employee
            mockEmployeeRepository.Setup(repo => repo.GetEmployee(employeeId))
                .ReturnsAsync(employeeFromRepo); // Returns the employee asynchronously

            CancellationToken cancellationToken = default;

            // Act
            var result = await getEmployeeByIdQueryHandler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeFromRepo.Id, result.Id);
            Assert.Equal(employeeFromRepo.Name, result.Name);
            Assert.Equal(employeeFromRepo.Address, result.Address);
            Assert.Equal(employeeFromRepo.Email, result.Email);
            Assert.Equal(employeeFromRepo.Department, result.Department);

            // Verify that GetEmployeeById was called exactly once
            mockEmployeeRepository.Verify(repo => repo.GetEmployee(employeeId), Times.Once);

            // Verify all mock expectations
            mockRepository.VerifyAll();
        }
    }
}
