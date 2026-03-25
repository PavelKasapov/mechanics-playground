using MechanicsPlayground.Core;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Free3DCamera
{
    public class Scope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new RotationSettings()).As<ISettings>().AsSelf();
            builder.RegisterInstance(new MovementSettings()).As<ISettings>().AsSelf();
            builder.RegisterInstance(new ZoomSettings()).As<ISettings>().AsSelf();
            builder.RegisterEntryPoint<Controller>(Lifetime.Singleton);
            builder.Register<InputAdapter>(Lifetime.Singleton);
            builder.Register<MovementHandler>(Lifetime.Singleton);
            builder.Register<RotationHandler>(Lifetime.Singleton);
            builder.Register<ZoomHandler>(Lifetime.Singleton);
        }
    }
}
