using IntergartionXUnitTest.Services.Employees.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using UniTestCaseApp.Data;

namespace IntergartionXUnitTest.Helper
{
    public class TestHelper : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestHelper(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public static string GetApiBaseUrl(string endpoint)
        {
            // You can modify the base URL depending on your environment (localhost, production, etc.)
            var baseUrl = "https://localhost:7083/";
            return $"{baseUrl}{endpoint}";
        }

        // Cleanup method to reset the database
        public void ResetDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Clear or reset the database
                // For example, delete all data or use some custom cleanup logic
                dbContext.Database.EnsureCreated();  // Ensure the database is created

                // Optionally use transactions for rollback if needed
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Delete data from each table (customize as per your models)
                        var employees = dbContext.Employees.ToList();
                        dbContext.Employees.RemoveRange(employees);
                        dbContext.SaveChanges();

                        // Optionally you could add more cleanup steps for other tables
                        // dbContext.SomeOtherEntity.RemoveRange(dbContext.SomeOtherEntity);
                        // dbContext.SaveChanges();


                        transaction.Commit(); // Commit the transaction (can be skipped if you want to roll back)
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback in case of error
                        throw;
                    }
                }
            }
        }

        public static object CreateBaseData()
        {
            var newEmployee = new
            {
                employee = new
                {
                    name = "John Doe",
                    address = "123 Main St",
                    email = "john.doe@example.com",
                    department = "HR"
                }
            };

            return newEmployee;
        }
    }
}
