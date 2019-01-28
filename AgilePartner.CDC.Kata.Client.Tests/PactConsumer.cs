using PactNet;
using PactNet.Mocks.MockHttpService;
using System;

namespace AgilePartner.CDC.Kata.Client.Tests
{
    public class PactConsumer : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 9222; } }
        public string MockProviderServiceBaseUri { get { return string.Format("http://localhost:{0}", MockServerPort); } }

        public PactConsumer()
        {
            PactBuilder = new PactBuilder(
                new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"..\pacts",
                LogDir = @"c:\temp\logs"
            });

            PactBuilder
              .ServiceConsumer("Alcoholic")
              .HasPactWith("Bar Api");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}