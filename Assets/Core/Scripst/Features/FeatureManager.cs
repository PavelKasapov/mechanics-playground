using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureManager : IInitializable
    {
        private readonly LifetimeScope _gameScope;
        private LifetimeScope _cameraScope;

        public FeatureManager(LifetimeScope gameScope/*, IObjectResolver resolver*/)
        {
            Debug.Log("FeatureManager");
            _gameScope = gameScope;
            /*var presenter = resolver.Resolve<UISettingsPanelPresenter>();*/
        }

        public void Initialize()
        {
            TestRun();
        }

        private void Activate3DCamera()
        {
            _cameraScope?.Dispose();
            _cameraScope = _gameScope.CreateChild<FreeCamera3D.Scope>();
        }

        private void TestRun()
        {
            Activate3DCamera();
        }
    }
}