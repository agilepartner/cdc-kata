using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace AgilePartner.CDC.Kata.Bar.Tests
{
    public class ApiTestFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public ApiTestFixture()
        {
            var builder = 
                new WebHostBuilder()
                   //.UseContentRoot(GetContentRootPath())
                   .UseEnvironment("Development")
                   .UseStartup<Startup>();  // Uses Start up class from your API Host project to configure the test server

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}