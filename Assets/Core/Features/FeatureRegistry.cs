using R3;

namespace MechanicsPlayground.Core
{
    public class FeatureRegistry : IFeatureRegistry, IFeatureRegistryWritter
    {
        private readonly ReactiveProperty<ICameraController> _activeCamera = new();

        ReadOnlyReactiveProperty<ICameraController> IFeatureRegistry.ActiveCamera => _activeCamera;

        public void SetActiveCamera(ICameraController camera) => _activeCamera.Value = camera;
    }
}
