using System.Collections.Generic;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureManager : IInitializable
    {
        private readonly LifetimeScope _gameScope;
        private readonly Dictionary<FeatureCategory, LifetimeScope> _activeModules = new();

        public FeatureManager(LifetimeScope gameScope)
        {
            _gameScope = gameScope;
        }

        public void Initialize()
        {
            ActivateModule<Free3DCamera.Scope>(FeatureCategory.Camera);
        }

        public void ActivateModule<TScope>(FeatureCategory category) where TScope : LifetimeScope
        {
            if (_activeModules.TryGetValue(category, out var module))
            {
                if (module is TScope)
                    return;

                _activeModules.Remove(category);
                module.Dispose();
            }
            _activeModules.Add(category, _gameScope.CreateChild<TScope>());
        }
    }
}