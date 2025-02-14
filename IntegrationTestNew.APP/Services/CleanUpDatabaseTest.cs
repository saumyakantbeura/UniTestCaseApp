using IntegrationTestNew.APP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestNew.APP.Services
{
    [TestClass]
    public class CleanUpDatabaseTest
    {
        private readonly HttpClient _httpClient;

        public CleanUpDatabaseTest()
        {
            // Initialize HttpClient using the TestHelper class
            _httpClient = TestHelper.GetHttpClient();
        }

        [TestMethod]
        public async Task TestDeleteAllEmployee()
        {
            // Send the DELETE request to delete the employee with the given ID
            var response = await _httpClient.DeleteAsync(TestHelper.GetApiBaseUrl($"Employee"));

            // Assert that the response status code is 204 No Content (indicating successful deletion)
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteEmployee()
        {
            // Define the employee ID to delete
            int employeeIdToDelete = 1;

            // Send the DELETE request to delete the employee with the given ID
            var response = await _httpClient.DeleteAsync(TestHelper.GetApiBaseUrl($"Employee/{employeeIdToDelete}"));

            // Assert that the response status code is 204 No Content (indicating successful deletion)
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
