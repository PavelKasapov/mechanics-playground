using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Camera3D
{
    public class Scope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MechanicsPlayground.Camera3D.Controller>(Lifetime.Singleton);
        }
    }
}
