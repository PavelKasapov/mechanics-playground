using Cysharp.Threading.Tasks;
using MechanicsPlayground.Core;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Follow3DCamera
{
    public class Scope : LifetimeScope
    {
        [SerializeField] private CinemachineCamera _camera;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera.transform).Keyed("CameraTransform");
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(new RotationSettings()).As<ISettings>().AsSelf();
            builder.RegisterEntryPoint<Controller>(Lifetime.Singleton);
            builder.Register<InputAdapter>(Lifetime.Singleton);
            builder.Register<RotationHandler>(Lifetime.Singleton);
            builder.RegisterEntryPoint<SimpleCameraFacade>(Lifetime.Singleton);
        }
    }
}
