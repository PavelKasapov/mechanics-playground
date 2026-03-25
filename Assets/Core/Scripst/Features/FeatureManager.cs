using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureManager : IInitializable
    {
        private readonly LifetimeScope _gameScope;
        private LifetimeScope _cameraScope;

        public FeatureManager(LifetimeScope gameScope)
        {
            _gameScope = gameScope;
        }

        public void Initialize()
        {
            TestRun();
        }

        private void Activate3DCamera()
        {
            _cameraScope?.Dispose();
            _cameraScope = _gameScope.CreateChild<Free3DCamera.Scope>();
        }

        private void ActivateOrthographic2DCamera()
        {
            _cameraScope?.Dispose();
            _cameraScope = _gameScope.CreateChild<Orthographic2DCamera.Scope>();
        }

        private void TestRun()
        {
            ActivateOrthographic2DCamera();
        }
    }
}