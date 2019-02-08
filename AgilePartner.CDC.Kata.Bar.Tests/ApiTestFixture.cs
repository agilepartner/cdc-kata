using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace AgilePartner.CDC.Kata.Bar.Tests
{
    public class ApiTestFixture : IDisposable
    {
        private readonly TestServer testServer;
        public HttpClient Client { get; }

        public ApiTestFixture()
        {
            var builder = 
                new WebHostBuilder()
                   .UseEnvironment("Development")
                   .UseStartup<Startup>();

            testServer = new TestServer(builder);
            Client = testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            testServer.Dispose();
        }
    }
}