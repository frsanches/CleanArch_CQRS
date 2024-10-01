using Banking.Api.IntegrationTests.TestBase;
using Banking.Contracts.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Banking.Api.IntegrationTests.ApiControllers
{
    public class CustomersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CustomersControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Should_CreateNewCustomer_WhenValidRequest()
        {
            // Arrange
            var requestBody = new CreateCustomerRequest("Jane", "Doe", "jane.doe@email.com", "546-35-9793");

            StringContent jsonContent = new(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            _client.DefaultRequestHeaders.Add("X-IdempotencyKey", Guid.NewGuid().ToString());

            // Act
            using HttpResponseMessage response = await _client.PostAsync("/api/customers", jsonContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateCustomerResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreateCustomerResponse>(result);
        }

        [Fact]
        public async Task Should_ReturnBadREquest_WhenNoIdempotencyHeader()
        {
            // Arrange
            var requestBody = new CreateCustomerRequest("Jane", "Doe", "jane.doe@email.com", "546-35-9793");

            StringContent jsonContent = new(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            // Act
            using HttpResponseMessage response = await _client.PostAsync("/api/customers", jsonContent);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ProblemDetails>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); 

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProblemDetails>(result);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_UpdateCustomerEmailAddress_WhenCustomerExists()
        {
            // Arrange
            var updateCustomer = new UpdateCustomerEmailRequest("jane.doe@email.com");

            StringContent jsonContent = new(
                JsonSerializer.Serialize(updateCustomer),
                Encoding.UTF8,
                "application/json");

            var customer = await CreateCustomer();

            // Act
            using HttpResponseMessage response = await _client.PutAsync($"/api/customers/{customer.Id}", jsonContent);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateCustomerResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateCustomer.Email, result.Email);
        }

        private async Task<CreateCustomerResponse> CreateCustomer()
        {
            var requestBody = new CreateCustomerRequest("Jane", "Doe", "jane.doe@email.com", "546-35-9793");

            StringContent jsonContent = new(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            _client.DefaultRequestHeaders.Add("X-IdempotencyKey", Guid.NewGuid().ToString());

            // Act
            using HttpResponseMessage response = await _client.PostAsync("/api/customers", jsonContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateCustomerResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
