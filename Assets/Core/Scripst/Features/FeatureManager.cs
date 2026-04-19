using System.Linq;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureManager : IInitializable
    {
        private readonly LifetimeScope _gameScope;
        private readonly IFeatureRegistryWritter _featureRegistry;

        public FeatureManager(LifetimeScope gameScope, IFeatureRegistryWritter featureRegistry)
        {
            _gameScope = gameScope;
            _featureRegistry = featureRegistry;
        }

        public void Initialize()
        {
            ActivateModule(_featureRegistry.AllModules.First(module => module.FeatureCategory == FeatureCategory.Camera));
        }

        public void ActivateModule(ModuleDefinition module)
        {
            if (_featureRegistry.ActiveModuleScopes.TryGetValue(module.FeatureCategory, out var scope))
            {
                _featureRegistry.ActiveModuleScopes.Remove(module.FeatureCategory);
                scope.Dispose();
            }
            _featureRegistry.ActiveModuleScopes.Add(module.FeatureCategory, _gameScope.CreateChildFromPrefab(module.ScopePrefab));
        }
    }
}