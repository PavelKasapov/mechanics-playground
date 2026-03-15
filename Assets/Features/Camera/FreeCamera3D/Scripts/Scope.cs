using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.FreeCamera3D
{
    public class Scope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new RotationSettings());
            builder.RegisterInstance(new MovementSettings());
            builder.RegisterInstance(new ZoomSettings());
            builder.RegisterEntryPoint<Controller>(Lifetime.Singleton);
            builder.Register<InputAdapter>(Lifetime.Singleton);
            builder.Register<MovementHandler>(Lifetime.Singleton);
            builder.Register<RotationHandler>(Lifetime.Singleton);
            builder.Register<ZoomHandler>(Lifetime.Singleton);
        }
    }
}
