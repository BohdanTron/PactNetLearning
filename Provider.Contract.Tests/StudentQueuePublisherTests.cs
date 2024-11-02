using System.Text.Json;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace Provider.Contract.Tests
{
    public class StudentQueuePublisherTests : IDisposable
    {
        private readonly PactVerifier _pactVerifier;

        public StudentQueuePublisherTests(ITestOutputHelper output)
        {
            _pactVerifier = new PactVerifier("Student Queue Publisher", new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XunitOutput(output)
                },
                LogLevel = PactLogLevel.Debug
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _pactVerifier.Dispose();
        }

        [Fact]
        public void EnsureEventPublisherHonorsPactWithConsumer()
        {
            var pactPath = Path.Combine("..", "..", "..", "..", "Consumer.Contract.Tests", "pacts", "Student Queue Listener-Student Queue Publisher.json");

            var defaultSettings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _pactVerifier
                .WithMessages(scenarios =>
                {
                    // register the responses to each interaction
                    // the descriptions must match those in the pact file(s)

                    scenarios.Add("a created student event", (builder) =>
                    {
                        builder
                            .WithMetadata(new
                            {
                                contentType = "application/json"
                            })
                            .WithContent(() =>
                                new StudentCreatedEvent
                                {
                                    StudentId = 10,
                                    FirstName = "James",
                                    LastName = "Hetfield",
                                    Gender = "male"
                                }
                            );
                    });
                }, defaultSettings)
                .WithFileSource(new FileInfo(pactPath))
                .Verify();
        }
    }
}
