using AgilePartner.CDC.Kata.Shared.Http;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgilePartner.CDC.Kata.Client.Tests
{
    public class alcoholic_should : IClassFixture<PactConsumer>
    {
        private readonly IMockProviderService mockProviderService;
        private readonly string serviceBaseUri;
        private readonly Alcoholic alcoholic;

        private const int ADULT = 18;
        private const int TOO_YOUNG = 17;
        private const string BEERS_PATH = "/bar/beers";

        public alcoholic_should(PactConsumer data)
        {
            mockProviderService = data.MockProviderService;
            mockProviderService.ClearInteractions();
            //Clears any previously registered interactions before the test is run
            serviceBaseUri = data.MockProviderServiceBaseUri;
            alcoholic = new Alcoholic(
                new RestProxy(),
                serviceBaseUri + BEERS_PATH);
        }

        [Fact]
        public async Task be_able_to_order_a_beer_when_is_old_enoughAsync()
        {
            mockProviderService
              .Given("A client is adult (age >= 18)")
              .UponReceiving("A POST request to order a beer")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Post,
                  Path = BEERS_PATH,
                  Headers = new Dictionary<string, object>
                  {
                    { "Content-Type", "application/json; charset=utf-8" },
                  },
                  Body = new
                  {
                      Age = 18
                  }
              })
              .WillRespondWith(new ProviderServiceResponse
              {
                  Status = 201,
                  Headers = new Dictionary<string, object>
                  {
                    { "Content-Type", "application/json; charset=utf-8" }
                  }
              });
                       
            await alcoholic.OrderAsync(ADULT);
            
            mockProviderService.VerifyInteractions(); 
        }

        [Fact]
        public async Task Not_be_able_to_order_a_beer_when_is_too_young()
        {
            mockProviderService
              .Given("A client is too young (age < 18)")
              .UponReceiving("A POST request to order a beer")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Post,
                  Path = BEERS_PATH,
                  Headers = new Dictionary<string, object>
                  {
                    { "Content-Type", "application/json; charset=utf-8" },
                  },
                  Body = new
                  {
                      Age = 17
                  }
              })
              .WillRespondWith(new ProviderServiceResponse
              {
                  Status = 401,
                  Headers = new Dictionary<string, object>
                  {
                    { "Content-Type", "application/json; charset=utf-8" }
                  }
              });

            await alcoholic.OrderAsync(TOO_YOUNG);

            mockProviderService.VerifyInteractions();
        }
    }
}
