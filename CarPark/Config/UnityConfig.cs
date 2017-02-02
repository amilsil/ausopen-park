using Microsoft.Practices.Unity;

namespace CarPark.Config
{
    public class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IRateCalculator, RateCalculator>();

            return container;
        }
    }
}
