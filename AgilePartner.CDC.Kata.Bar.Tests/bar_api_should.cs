using PactNet;
using PactNet.Infrastructure.Outputters;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace AgilePartner.CDC.Kata.Bar.Tests
{
    public class bar_api_should : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture fixture;
        private readonly ITestOutputHelper output;

        public bar_api_should(
            ApiTestFixture fixture,
            ITestOutputHelper output)
        {
            this.fixture = fixture;
            this.output = output;
        }

        [Fact]
        public void respect_its_contract_with_the_alcoholic()
        {
            var serverUri = "http://localhost:9222/api";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> { new XUnitOutput(output) }
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("Bar Api", serverUri)
                .HonoursPactWith("Alcoholic")
                .PactUri("https://raw.githubusercontent.com/agilepartner/cdc-kata/2_create_bar_service/AgilePartner.CDC.Kata.Client.Tests/pacts/alcoholic-bar_api.json")
                .Verify();
        }
    }
}
