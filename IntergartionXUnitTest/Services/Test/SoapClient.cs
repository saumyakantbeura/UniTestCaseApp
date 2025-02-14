using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class SoapApiTests
{
    [Fact]
    public async Task TestCelsiusToFahrenheitConversion()
    {
        // Create an HttpClient instance
        using var client = new HttpClient();

        // Define the SOAP request XML for the CelsiusToFahrenheit operation
        var soapRequestXml = @"
        <?xml version=""1.0"" encoding=""utf-8""?>
        <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                         xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                         xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
            <soap12:Body>
                <CelsiusToFahrenheit xmlns=""http://www.w3schools.com/xml/"">
                    <Celsius>25</Celsius>
                </CelsiusToFahrenheit>
            </soap12:Body>
        </soap12:Envelope>";

        // Create the HTTP request content with the SOAP XML and set content type to "application/soap+xml"
        var content = new StringContent(soapRequestXml, Encoding.UTF8, "application/soap+xml");

        // SOAP endpoint URL
        var soapApiUrl = "https://www.w3schools.com/xml/tempconvert.asmx";

        // Set the SOAPAction header for the CelsiusToFahrenheit operation
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("SOAPAction", "\"http://www.w3schools.com/xml/CelsiusToFahrenheit\"");

        try
        {
            // Send the POST request to the SOAP API
            var response = await client.PostAsync(soapApiUrl, content);

            // Read the response as a string
            var responseString = await response.Content.ReadAsStringAsync();

            // Log the response status and body for debugging purposes
            Console.WriteLine("Response Status: " + response.StatusCode);
            Console.WriteLine("Response Body: " + responseString);

            // Assert that the response status is OK (200)
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Assert.False(true, $"Expected OK (200) but got {response.StatusCode}. Response body: {responseString}");
            }

            // Optionally, extract the Fahrenheit value from the response if available
            if (responseString.Contains("<CelsiusToFahrenheitResult>"))
            {
                var start = responseString.IndexOf("<CelsiusToFahrenheitResult>") + 27;
                var end = responseString.IndexOf("</CelsiusToFahrenheitResult>");
                var fahrenheitValue = responseString.Substring(start, end - start);
                Console.WriteLine($"The result in Fahrenheit is: {fahrenheitValue}");

                // Assert that the result is the expected Fahrenheit value (77 for 25°C)
                Assert.Equal("77", fahrenheitValue);
            }
            else
            {
                // If the result was not found, fail the test
                Assert.False(true, "The response did not contain the expected result.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception if something went wrong
            Console.WriteLine("Exception: " + ex.Message);
            Assert.False(true, "An exception occurred while sending the SOAP request.");
        }
    }
}
