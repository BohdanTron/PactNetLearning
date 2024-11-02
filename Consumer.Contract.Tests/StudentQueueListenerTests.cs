using System.Text.Json;
using FluentAssertions;
using PactNet;
using PactNet.Matchers;
using PactNet.Output.Xunit;
using Xunit.Abstractions;

namespace Consumer.Contract.Tests
{
    public class StudentQueueListenerTests
    {
        private readonly IMessagePactBuilderV3 _messagePactBuilder;

        public StudentQueueListenerTests(ITestOutputHelper output)
        {
            _messagePactBuilder = Pact.V3("Student Queue Listener", "Student Queue Publisher", new PactConfig
            {
                PactDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "/pacts",
                DefaultJsonSettings = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                },
                Outputters = [new XunitOutput(output)],
                LogLevel = PactLogLevel.Debug
            }).WithMessageInteractions();
        }

        [Fact]
        public void ReceiveStudentCreatedEvent()
        {
            _messagePactBuilder
                .ExpectsToReceive("a created student event")
                .Given("a student is pushed to the queue")
                    .WithJsonContent(new
                    {
                        StudentId = Match.Integer(10),
                        FirstName = "James",
                        LastName = "Hetfield",
                        Gender = "male",
                    })
                .Verify<StudentCreatedEvent>(message =>
                {
                    message.Should().BeEquivalentTo(new StudentCreatedEvent
                    {
                        StudentId = 10,
                        FirstName = "James",
                        LastName = "Hetfield",
                        Gender = "male",
                    });
                });
        }
    }
}
