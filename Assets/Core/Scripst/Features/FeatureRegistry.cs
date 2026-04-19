using ObservableCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureRegistry : IFeatureRegistry, IFeatureRegistryWritter
    {
        private List<ModuleDefinition> _allModules;
        public ObservableDictionary<FeatureCategory, LifetimeScope> ActiveModuleScopes { get; } = new();
        public IReadOnlyList<ModuleDefinition> AllModules => _allModules;
        public IReadOnlyObservableDictionary<FeatureCategory, LifetimeScope> ActiveModuleScopesReadOnly => ActiveModuleScopes;

        public FeatureRegistry()
        {
            _allModules = Resources.LoadAll<ModuleDefinition>("").ToList();
        } 
    }
}