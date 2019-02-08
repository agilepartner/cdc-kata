using AgilePartner.CDC.Kata.Bar.Exceptions;
using AgilePartner.CDC.Kata.Commands;

namespace AgilePartner.CDC.Kata.Bar
{
    internal class BarService : IBarService
    {
        public void GiveBeer(GiveMeABeer giveMeABeer)
        {
            if(giveMeABeer.Age < 18)
            {
                throw new NotAuthorizedException();
            }
        }
    }
}