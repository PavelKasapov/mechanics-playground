using MechanicsPlayground.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.HumanoidPlayer
{
    public class Scope : LifetimeScope
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _pivot;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_player).Keyed("PlayerTransform");
            builder.RegisterInstance(_pivot).Keyed("PivotTransform");
            builder.RegisterInstance(new MovementSettings()).As<ISettings>().AsSelf();
            builder.RegisterEntryPoint<Controller>(Lifetime.Singleton);
            builder.Register<InputAdapter>(Lifetime.Singleton);
            builder.Register<MovementHandler>(Lifetime.Singleton);
        }
    }
}
