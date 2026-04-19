using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class HelpWindowPresenter : IInitializable, IDisposable
    {
        private readonly IFeatureRegistry _featureRegistry;
        private readonly SimpleMonobehaviourFactory<FeatureButton> _buttonFactory;
        private readonly HelpWindow _helpWindow;
        private readonly Dictionary<ModuleDefinition, GameObject> _helpViews = new();
        private readonly Dictionary<ModuleDefinition, FeatureButton> _buttons = new();
        private readonly Dictionary<Type, ModuleDefinition> _modulesByType = new();
        private readonly CompositeDisposable _disposables = new();

        private ModuleDefinition _currentModule;

        public HelpWindowPresenter(IFeatureRegistry featureRegistry,
            [Key("HelpButtonFactory")]SimpleMonobehaviourFactory<FeatureButton> buttonFactory, 
            HelpWindow helpWindow)
        {
            _featureRegistry = featureRegistry;
            _buttonFactory = buttonFactory;
            _helpWindow = helpWindow;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Initialize()
        {
            var generalHelpButton = _buttonFactory.Create(_helpWindow.ActiveModulesPlaceholder);
            generalHelpButton.Init("General Help", () =>
            {
                if (_currentModule != null)
                    _helpViews[_currentModule].SetActive(false);
            });

            foreach (var module in _featureRegistry.AllModules)
            {
                var moduleType = module.ScopePrefab.GetType();
                _modulesByType.Add(moduleType, module);

                var _helpView = GameObject.Instantiate(module.HelpPrefab, _helpWindow.DynamicHelpArea);
                _helpView.SetActive(false);
                _helpViews.Add(module, _helpView);

                var button = _buttonFactory.Create(_helpWindow.InactiveModulesPlaceholder);
                _buttons.Add(module, button);

                button.Init(module.DisplayName, () =>
                {
                    if (_currentModule != null) 
                        _helpViews[_currentModule].SetActive(false);

                    _helpViews[module].SetActive(true);
                    _currentModule = module;
                });
            }

            _featureRegistry.ActiveModuleScopesReadOnly.ObserveAdd().Subscribe(ev => OnDictionaryAdd(ev.Value)).AddTo(_disposables);
            _featureRegistry.ActiveModuleScopesReadOnly.ObserveRemove().Subscribe(ev => OnDictionaryRemove(ev.Value)).AddTo(_disposables);

            foreach (var kvp in _featureRegistry.ActiveModuleScopesReadOnly)
            {
                OnDictionaryAdd(kvp);
            }
        }

        private void OnDictionaryAdd(KeyValuePair<FeatureCategory, LifetimeScope> kvp)
        {
            var scope = kvp.Value;
            var scopeType = scope.GetType();
            var button = _buttons[_modulesByType[scopeType]];
            button.transform.SetParent(_helpWindow.ActiveModulesPlaceholder);
        }

        private void OnDictionaryRemove(KeyValuePair<FeatureCategory, LifetimeScope> kvp)
        {
            var scope = kvp.Value;
            var scopeType = scope.GetType();
            var button = _buttons[_modulesByType[scopeType]];
            var buttonTransform = button.transform;
            buttonTransform.transform.SetParent(_helpWindow.InactiveModulesPlaceholder);
            buttonTransform.SetAsFirstSibling();
            if (_currentModule == _modulesByType[scopeType])
            {
                _helpViews[_currentModule].SetActive(false);
                _helpViews.First().Value.SetActive(true);
            }
        }
    }
}