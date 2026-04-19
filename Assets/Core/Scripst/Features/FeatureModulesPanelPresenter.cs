using MechanicsPlayground.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class FeatureModulesPanelPresenter : IInitializable
    {
        private readonly FeatureManager _featureManager;
        private readonly Transform _panelView;
        private readonly SimpleMonobehaviourFactory<FeatureModuleGroup> _featureGroupFactory;
        private readonly SimpleMonobehaviourFactory<FeatureButton> _featureButtonFactory;
        private readonly IFeatureRegistry _featureRegistry;

        public FeatureModulesPanelPresenter(
            FeatureManager featureManager,
            [Key("FeatureModulesPanel")] Transform panelView,
            SimpleMonobehaviourFactory<FeatureModuleGroup> featureGroupFactory,
            SimpleMonobehaviourFactory<FeatureButton> featureButtonFactory,
            IFeatureRegistry featureRegistry)
        {
            _featureManager = featureManager;
            _panelView = panelView;
            _featureGroupFactory = featureGroupFactory;
            _featureButtonFactory = featureButtonFactory;
            _featureRegistry = featureRegistry;
        }

        public void Initialize()
        {
            var groups = _featureRegistry.AllModules.GroupBy(m => m.FeatureCategory);

            foreach (var group in groups)
            {
                var groupView = _featureGroupFactory.Create(_panelView);
                var buttons = new List<FeatureButton>();

                foreach (var module in group)
                {
                    var button = _featureButtonFactory.Create(groupView.transform);
                    button.Init(module.DisplayName, () => _featureManager.ActivateModule(module));
                    buttons.Add(button);
                }

                groupView.Init(group.Key.ToString(), buttons);
            }
        }
    }
}
