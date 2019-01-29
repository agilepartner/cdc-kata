using PactNet;
using PactNet.Infrastructure.Outputters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AgilePartner.CDC.Kata.Bar.Tests
{
    public class api_should : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture fixture;
        private readonly ITestOutputHelper output;

        public api_should(
            ApiTestFixture fixture,
            ITestOutputHelper output)
        {
            this.fixture = fixture;
            this.output = output;
        }

        [Fact]
        public async Task EnsureSomethingApiHonoursPactWithConsumer()
        {
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> 
                //NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output, so a custom outputter is required.
                {
                    new XUnitOutput(output)
                },
                Verbose = true
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ProviderState($"{serviceUri}/provider-states")
                .ServiceProvider("Bar Api", serviceUri)
                .HonoursPactWith("Alcoholic")
                .PactUri("..\\..\\..\\AgilePartner.CDC.Kata.Client.Tests\\bin\\Debug\\pacts\\alcoholic-bar_api.json")
                .Verify();

            var response = await fixture.Client.GetAsync("api/bar/beers");
            response.EnsureSuccessStatusCode();

            var responseStrong = await response.Content.ReadAsStringAsync();
        }

        private string GetContentRootPath()
        {
            throw new NotImplementedException();
        }
    }
}
