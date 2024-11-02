using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Provider.Contract.Tests
{
    public class StudentsApiFixture : IAsyncLifetime
    {
        private IHost _server;

        public Uri ServerUri { get; } = new("http://localhost:9001");

        public async Task InitializeAsync()
        {
            _server = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(ServerUri.ToString());
                    webBuilder.UseStartup<TestStartup>();
                })
                .Build();

            await _server.StartAsync();
        }

        public async Task DisposeAsync()
        {
            _server.Dispose();
        }
    }
}
