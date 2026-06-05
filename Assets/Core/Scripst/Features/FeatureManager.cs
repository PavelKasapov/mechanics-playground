using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureManager : IInitializable
    {
        private readonly LifetimeScope _gameScope;
        private readonly IFeatureRegistryWritter _featureRegistry;
        private readonly CameraHandler _cameraHandler;
        private readonly CinemachineCamera _mockCamera;

        public FeatureManager(
            LifetimeScope gameScope, 
            IFeatureRegistryWritter featureRegistry, 
            CameraHandler cameraHandler, 
            [Key("MockCamera")] CinemachineCamera mockCamera)
        {
            _gameScope = gameScope;
            _featureRegistry = featureRegistry;
            _cameraHandler = cameraHandler;
            _mockCamera = mockCamera;
        }

        public void Initialize()
        {
            //ActivateModule(_featureRegistry.AllModules.First(module => module.FeatureCategory == FeatureCategory.Camera));
        }

        public async UniTaskVoid ActivateModule(ModuleDefinition module)
        {
            if (_featureRegistry.ActiveModuleScopes.TryGetValue(module.FeatureCategory, out var scope))
            {
                if (scope.GetType() == module.ScopePrefab.GetType())
                    return;


                switch (module.FeatureCategory)
                {
                    case FeatureCategory.Camera:
                        if (_cameraHandler.CameraFacade == null)
                            break;

                        _mockCamera.Lens = _cameraHandler.CameraFacade.CinemachineCamera.Lens;
                        _mockCamera.ForceCameraPosition(
                            _cameraHandler.CameraFacade.CinemachineCamera.transform.position,
                            _cameraHandler.CameraFacade.CinemachineCamera.transform.rotation);

                        _mockCamera.Priority = 10;
                        
                        _featureRegistry.ActiveModuleScopes.Remove(module.FeatureCategory);
                        scope.Dispose();

                        _featureRegistry.ActiveModuleScopes.Add(module.FeatureCategory, _gameScope.CreateChildFromPrefab(module.ScopePrefab));

                        await UniTask.WaitUntil(() => _cameraHandler.CameraFacade.CinemachineCamera.Lens.Orthographic == _cameraHandler.CameraFacade.CinemachineCamera.State.Lens.Orthographic);
                        _mockCamera.Priority = -1;
                        break;
                    default:
                        _featureRegistry.ActiveModuleScopes.Remove(module.FeatureCategory);
                        scope.Dispose();

                        _featureRegistry.ActiveModuleScopes.Add(module.FeatureCategory, _gameScope.CreateChildFromPrefab(module.ScopePrefab));
                        break;
                }
            }
            else
            {
                _featureRegistry.ActiveModuleScopes.Add(module.FeatureCategory, _gameScope.CreateChildFromPrefab(module.ScopePrefab));
            }
        }
    }
}