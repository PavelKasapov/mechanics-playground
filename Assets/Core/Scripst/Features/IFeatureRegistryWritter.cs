using ObservableCollections;
using System.Collections.Generic;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public interface IFeatureRegistryWritter
    {
        public IReadOnlyList<ModuleDefinition> AllModules { get; }
        public ObservableDictionary<FeatureCategory, LifetimeScope> ActiveModuleScopes { get; }
    }   
}