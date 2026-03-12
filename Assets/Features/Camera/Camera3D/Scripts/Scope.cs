using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Camera3D
{
    public class Scope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Controller>(Lifetime.Singleton);
            builder.Register<InputAdapter>(Lifetime.Singleton);
        }
    }
}
