using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace Provider.Contract.Tests
{
    public class StudentsApiTests : IClassFixture<StudentsApiFixture>
    {
        private readonly StudentsApiFixture _fixture;
        private readonly ITestOutputHelper _output;

        public StudentsApiTests(StudentsApiFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public void EnsureStudentsApiHonorsPactWithConsumer()
        {
            // Arrange
            var pactConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XunitOutput(_output)
                },
                LogLevel = PactLogLevel.Debug
            };

            var pactPath = Path.Combine("..", "..", "..", "..", "Consumer.Contract.Tests", "pacts", "StudentApiClient-StudentApi.json");

            // Act / Assert
            var pactVerifier = new PactVerifier("StudentApi", pactConfig);

            pactVerifier
                .WithHttpEndpoint(_fixture.ServerUri)
                //.WithFileSource(new FileInfo(pactPath))
                .WithPactBrokerSource(new Uri("https://btron.pactflow.io"), options =>
                {
                    options.TokenAuthentication("5U40qcCjDyMyqmLQN3IbYg");
                    options.PublishResults(true, "1.0.1");
                })
                .WithProviderStateUrl(new Uri(_fixture.ServerUri, "/provider-states"))
                .Verify();
        }
    }
}