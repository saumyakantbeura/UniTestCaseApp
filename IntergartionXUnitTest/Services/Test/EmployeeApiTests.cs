using IntergartionXUnitTest.Helper;
using IntergartionXUnitTest.Services.Employees.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace IntergartionXUnitTest.Services.Test
{
    public class EmployeeApiTests: IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;
        private readonly TestHelper _testHelper;
        public EmployeeApiTests(WebApplicationFactory<Program> _webAppFactory)
        {
            _client = _webAppFactory.CreateDefaultClient();
            _testHelper = new TestHelper(_webAppFactory);
        }
              

        [Fact]
        public async Task TestCreateEmployee()
        {
            _testHelper.ResetDatabase();

            // Define the new employee object to be posted
            var employee = TestHelper.CreateBaseData();

            // Serialize the employee object to JSON
            var jsonContent = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send a POST request to create the employee
            var response = await _client.PostAsync(TestHelper.GetApiBaseUrl("Employee"), content);

            // Assert that the status code is 200 OK (or other success status depending on your API design)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Optionally, you can deserialize the response content to check the returned employee object
            var responseString = await response.Content.ReadAsStringAsync();
            var createdEmployee = JsonConvert.DeserializeObject<Employee>(responseString);

            // Assert that the created employee's properties are set as expected
            Assert.NotNull(createdEmployee);
            Assert.Equal("John Doe", createdEmployee.Name);
            Assert.Equal("john.doe@example.com", createdEmployee.Email);

        }

        [Fact]
        public async Task TestGetAllEmployees()
        {
            // Assuming TestHelper.GetApiBaseUrl() returns a properly configured HttpClient
            var url = TestHelper.GetApiBaseUrl("Employee"); // This should return the full URL

            // Ensure you are using HttpClient to send the GET request
            var response = await _client.GetAsync(url);

            // Assert that the response status code is OK (200)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Optionally, you can read the content of the response
            var result = await response.Content.ReadAsStringAsync();

            // Assert that the result is not null or empty
            Assert.False(string.IsNullOrEmpty(result));

            // Optionally, you can check if the result contains expected data
            // For example, if you expect employees to have a specific name:
            // Assert.IsTrue(result.Contains("John Doe"));

            // If you expect a list of employees, you can deserialize the response content:
            var employees = JsonConvert.DeserializeObject<List<Employee>>(result);

            // For example, check the count of employees
            Assert.True(employees.Count > 0);
        }

        


    }
}