using IntegrationTestNew.APP.Helpers;
using IntegrationTestNew.APP.Services.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestNew.APP.Services
{
    [TestClass]
    public class ConfigureEmployeeDataTest
    {
        private readonly HttpClient _httpClient;

        public ConfigureEmployeeDataTest()
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

    }
}
