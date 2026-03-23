using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MechanicsPlayground.Core
{
    public class SettingsRegistry : IDisposable
    {
        public ObservableDictionary<string, ModuleSettingsGroup> ModulesByName { get; } = new();
        
        public IDisposable RegisterModule(string moduleName, IEnumerable<ISettingsDescriptor> settings)
        {
            if (ModulesByName.ContainsKey(moduleName))
                throw new InvalidOperationException($"Module {moduleName} already registered");

            var newGroup = new ModuleSettingsGroup(moduleName, settings);

            ModulesByName.Add(moduleName, newGroup);

            Debug.Log($"Registered settings of {ModulesByName.First().Key} module");
            Debug.Log($"settings.Count() {settings.Count()}");

            return Disposable.Create(() => UnregisterModule(moduleName));
        }

        public void UnregisterModule(string moduleName)
        {
            if (ModulesByName.TryGetValue(moduleName, out var group))
            {
                ModulesByName.Remove(moduleName);
            }
        }

        public void Dispose()
        {
            ModulesByName.Clear();
        }
    }
}