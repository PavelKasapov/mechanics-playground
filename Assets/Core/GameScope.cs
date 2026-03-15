using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(Camera.main.transform).Keyed("CameraTransform");
            builder.RegisterInstance(Camera.main);

            builder.Register<FeatureRegistry>(Lifetime.Singleton)
                .As<IFeatureRegistry>()
                .As<IFeatureRegistryWritter>();

            builder.RegisterEntryPoint<FeatureManager>();
        }
    }
}
