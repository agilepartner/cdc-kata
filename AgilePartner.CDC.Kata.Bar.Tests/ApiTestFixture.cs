using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace AgilePartner.CDC.Kata.Bar.Tests
{
    public class ApiTestFixture : IDisposable
    {
        private IWebHost host;

        public ApiTestFixture()
        {
            var builder = 
                new WebHostBuilder()
                   .UseEnvironment("Development")
                   .UseKestrel()
                   .UseUrls("http://localhost:9222/")
                   .UseStartup<Startup>();

            host = builder.Build();
            host.Start();
        }

        public void Dispose()
        {
            host.StopAsync();
        }
    }
}