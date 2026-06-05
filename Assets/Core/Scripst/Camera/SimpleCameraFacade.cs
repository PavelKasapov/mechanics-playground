using Unity.Cinemachine;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class SimpleCameraFacade : ICameraFacade, IInitializable
    {
        private readonly CinemachineCamera _camera;
        private readonly CameraHandler _cameraHandler;
        public CinemachineCamera CinemachineCamera => _camera;

        public SimpleCameraFacade(CinemachineCamera camera, CameraHandler cameraHandler)
        {
            _camera = camera;
            _cameraHandler = cameraHandler;
        }

        public virtual void Initialize()
        {
            _cameraHandler.CameraFacade = this;
        }
    }
}