using MechanicsPlayground.Core;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Tests.Core
{
    /*public class UISettingsPanelPresenter : IDisposable, IInitializable
    {
        private readonly SettingsRegistry _settingsRegistry;
        private readonly SettingControlProvider _settingControlProvider;
        private readonly Dictionary<string, SettingsControlModule> _settingsModule = new();
        private readonly ObjectPool<SettingsControlModule> _settingsModulePool;
        private readonly SettingsControlModule _modulePrefab;
        private readonly Transform _panelView;
        private readonly CompositeDisposable _compositeDisposable = new();

        public UISettingsPanelPresenter(
            SettingsRegistry settingsRegistry,
            SettingControlProvider settingControlProvider,
            SettingsControlModule modulePrefab,
            [Key("UISettingsPanel")] Transform panelView)
        {
            _settingsRegistry = settingsRegistry;
            _settingControlProvider = settingControlProvider;
            _modulePrefab = modulePrefab;
            _panelView = panelView;

            _settingsRegistry.ModulesByName.ObserveAdd().Subscribe(ev => OnModuleAdded(ev.Value.Key, ev.Value.Value)).AddTo(_compositeDisposable);
            _settingsRegistry.ModulesByName.ObserveRemove().Subscribe(ev => OnModuleRemoved(ev.Value.Key, ev.Value.Value)).AddTo(_compositeDisposable);

            _settingsModulePool = new(() => GameObject.Instantiate(_modulePrefab, _panelView),
                module => module.gameObject.SetActive(true),
                module => module.gameObject.SetActive(false),
                defaultCapacity: 3);

            foreach (var item in _settingsRegistry.ModulesByName)
            {
                OnModuleAdded(item.Key, item.Value);
            }
        }

        private void OnModuleAdded(string moduleName, ModuleSettingsGroup moduleSettingsGroup)
        {
            if (moduleSettingsGroup.Settings == null || !moduleSettingsGroup.Settings.Any())
            {
                Debug.LogWarning($"Module {moduleName} has no settings");
                return;
            }

            var newModule = _settingsModulePool.Get();
            var settingsControlList = moduleSettingsGroup.Settings.Select(descriptor => _settingControlProvider.GetSettingControl(descriptor)).ToList();

            newModule.Init(moduleName, settingsControlList);
            _settingsModule.Add(moduleName, newModule);
        }

        private void OnModuleRemoved(string moduleName, ModuleSettingsGroup moduleSettingsGroup)
        {
            var moduleToRemove = _settingsModule[moduleName];
            moduleToRemove.SettingControls.ForEach(settingsControl => _settingControlProvider.ReturnSettingControl(settingsControl));
            moduleToRemove.SettingControls.Clear();
            _settingsModule.Remove(moduleName);
            _settingsModulePool.Release(moduleToRemove);
        }
        public void Dispose() => _compositeDisposable?.Dispose();

        public void Initialize() { }
    }*/
}
