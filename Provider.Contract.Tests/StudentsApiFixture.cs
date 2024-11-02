using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Provider.Contract.Tests.Middlewares;

namespace Provider.Contract.Tests
{
    public class StudentsApiFixture : IAsyncLifetime
    {
        private WebApplication _server;

        public Uri ServerUri { get; } = new("http://localhost:9001");

        public async Task InitializeAsync()
        {
            var builder = WebApplication.CreateBuilder()
                .ConfigureServices();

            builder.WebHost.UseUrls(ServerUri.ToString());

            _server = builder.Build();

            _server.UseMiddleware<ProviderStateMiddleware>();
            _server.Configure();

            await _server.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _server.DisposeAsync();
        }
    }
}
