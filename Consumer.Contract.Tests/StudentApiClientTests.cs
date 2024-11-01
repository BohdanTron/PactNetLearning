using System.Net;
using FluentAssertions;
using PactNet;
using PactNet.Output.Xunit;
using Xunit.Abstractions;

namespace Consumer.Contract.Tests
{
    public class StudentApiClientTests
    {
        private readonly IPactBuilderV4 _pactBuilder;

        public StudentApiClientTests(ITestOutputHelper output)
        {
            _pactBuilder = Pact.V4("StudentApiClient", "StudentApi", new PactConfig
            {
                PactDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "/pacts",
                Outputters = [ new XunitOutput(output)],
                LogLevel = PactLogLevel.Debug
            }).WithHttpInteractions();
        }

        [Fact]
        public async Task GetById_Exist()
        {
            // Arrange
            var expectedStudent = new  { id = 10, firstName = "Lars", lastName = "Ulrich", address = "1235, 57th Street, New York, USA" };

            _pactBuilder
                .UponReceiving("a request to get a student")
                    .Given("student with id 10 exists")
                    .WithRequest(HttpMethod.Get, "/students/10")
                    .WithHeader("Accept", "application/json")
                .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(expectedStudent);

            await _pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var apiClient = new StudentApiClient(ctx.MockServerUri);
                var product = await apiClient.GetStudentById(10);

                // Assert
                product?.Id.Should().Be(10);
            });
        }
    }
}