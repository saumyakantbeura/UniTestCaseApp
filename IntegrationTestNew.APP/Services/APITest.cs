using IntegrationTestNew.APP.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestNew.APP.Services.Model;

namespace IntegrationTestNew.APP.Services
{
    [TestClass]
    public class APITest
    {
        private readonly HttpClient _httpClient;

        public APITest()
        {
            // Initialize HttpClient using the TestHelper class
            _httpClient = TestHelper.GetHttpClient();
        }


        [TestMethod]
        public async Task TestCreateEmployee()
        {
            // Define the new employee object to be posted
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

            // Serialize the employee object to JSON
            var jsonContent = JsonConvert.SerializeObject(newEmployee);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send a POST request to create the employee
            var response = await _httpClient.PostAsync(TestHelper.GetApiBaseUrl("Employee"), content);

            // Assert that the status code is 200 OK (or other success status depending on your API design)
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Optionally, you can deserialize the response content to check the returned employee object
            var responseString = await response.Content.ReadAsStringAsync();
            var createdEmployee = JsonConvert.DeserializeObject<Employee>(responseString);

            // Assert that the created employee's properties are set as expected
            Assert.IsNotNull(createdEmployee);
            Assert.AreEqual("John Doe", createdEmployee.Name);
            Assert.AreEqual("john.doe@example.com", createdEmployee.Email);

        }

        [TestMethod]
        public async Task TestGetAllEmployees()
        {
            // Assuming TestHelper.GetApiBaseUrl() returns a properly configured HttpClient
            var url = TestHelper.GetApiBaseUrl("Employee"); // This should return the full URL

            // Ensure you are using HttpClient to send the GET request
            var response = await _httpClient.GetAsync(url);

            // Assert that the response status code is OK (200)
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Optionally, you can read the content of the response
            var result = await response.Content.ReadAsStringAsync();

            // Assert that the result is not null or empty
            Assert.IsFalse(string.IsNullOrEmpty(result));

            // Optionally, you can check if the result contains expected data
            // For example, if you expect employees to have a specific name:
            // Assert.IsTrue(result.Contains("John Doe"));

            // If you expect a list of employees, you can deserialize the response content:
            var employees = JsonConvert.DeserializeObject<List<Employee>>(result);

            // For example, check the count of employees
            Assert.IsTrue(employees.Count > 0);
        }



        [TestMethod]
        public async Task TestGet_EmployeeById()
        {
            // Specify the URL to test using the helper method
            var url = TestHelper.GetApiBaseUrl("Employee/2");

            // Make the GET request to the URL
            var response = await _httpClient.GetAsync(url);

            // Read the content of the response
            var result = await response.Content.ReadAsStringAsync();

            // Assert the response is not null
            Assert.IsNotNull(result);

            // You can add more assertions here based on your expected response
            // For example, if you expect a specific employee name:
            // Assert.AreEqual("Expected Employee Name", result);
        }


       
    }


}
