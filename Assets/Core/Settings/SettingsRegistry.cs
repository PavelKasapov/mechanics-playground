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
        private readonly ObservableDictionary<string, ModuleSettingsGroup> _modulesByName = new();
        
        public IDisposable RegisterModule(string moduleName, IEnumerable<ISettingsDescriptor> settings)
        {
            if (_modulesByName.ContainsKey(moduleName))
                throw new InvalidOperationException($"Module {moduleName} already registered");

            var newGroup = new ModuleSettingsGroup(moduleName, settings);

            _modulesByName.Add(moduleName, newGroup);

            Debug.Log($"Registered settings of {_modulesByName.First().Key} module");

            return Disposable.Create(() => UnregisterModule(moduleName));
        }

        public void UnregisterModule(string moduleName)
        {
            if (_modulesByName.TryGetValue(moduleName, out var group))
            {
                _modulesByName.Remove(moduleName);
            }
        }

        public void Dispose()
        {
            _modulesByName.Clear();
        }
    }
}