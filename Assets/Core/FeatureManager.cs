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
            _cameraScope = _gameScope.CreateChild<Camera3D.Scope>();
        }

        private void TestRun()
        {
            Activate3DCamera();
        }
    }
}