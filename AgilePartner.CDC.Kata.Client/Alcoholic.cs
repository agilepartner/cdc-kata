using AgilePartner.CDC.Kata.Commands;
using AgilePartner.CDC.Kata.Shared.Http;
using System.Threading.Tasks;

namespace AgilePartner.CDC.Kata.Client
{
    public class Alcoholic
    {
        private readonly IRestProxy restProxy;
        private readonly string baseUri;

        public Alcoholic(
            IRestProxy restProxy,
            string baseUri)
        {
            this.restProxy = restProxy;
            this.baseUri = baseUri;
        }

        public async Task OrderAsync(int age)
        {
            var command = new GiveMeABeer
            {
                Age = age
            };
            await restProxy.PostAsync(baseUri, command);
        }
    }
}