using ObservableCollections;
using System.Collections.Generic;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public interface IFeatureRegistry
    {
        public IReadOnlyList<ModuleDefinition> AllModules { get; }
        public IReadOnlyObservableDictionary<FeatureCategory, LifetimeScope> ActiveModuleScopesReadOnly { get; }
    }
}