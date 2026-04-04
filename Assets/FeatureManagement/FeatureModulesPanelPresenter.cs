using MechanicsPlayground.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.FeatureManagement
{
    public class FeatureModulesPanelPresenter : IInitializable
    {
        private readonly FeatureManager _featureManager;
        private readonly Transform _panelView;
        private readonly SimpleMonobehaviourFactory<FeatureModuleGroup> _featureGroupFactory;
        private readonly SimpleMonobehaviourFactory<FeatureButton> _featureButtonFactory;

        private readonly (Type type, FeatureCategory category, string name)[] _featureModules = new (Type, FeatureCategory, string)[]
        {
            (typeof(Free3DCamera.Scope), FeatureCategory.Camera, "Free 3D Camera"),
            (typeof(Orthographic2DCamera.Scope), FeatureCategory.Camera, "Orthographic 2D Camera"),
        };

        public FeatureModulesPanelPresenter(
            FeatureManager featureManager,
            [Key("FeatureModulesPanel")] Transform panelView,
            SimpleMonobehaviourFactory<FeatureModuleGroup> featureGroupFactory,
            SimpleMonobehaviourFactory<FeatureButton> featureButtonFactory)
        {
            _featureManager = featureManager;
            _panelView = panelView;
            _featureGroupFactory = featureGroupFactory;
            _featureButtonFactory = featureButtonFactory;
        }

        public void Initialize()
        {
            var groups = _featureModules.GroupBy(m => m.category);

            foreach (var group in groups)
            {
                var groupView = _featureGroupFactory.Create(_panelView);
                var buttons = new List<FeatureButton>();

                foreach (var module in group)
                {
                    var button = _featureButtonFactory.Create(groupView.transform);

                    var method = typeof(FeatureManager).GetMethod(nameof(FeatureManager.ActivateModule));
                    var genericMethod = method.MakeGenericMethod(module.type);
                    var action = (Action<FeatureCategory>)Delegate.CreateDelegate(
                        typeof(Action<FeatureCategory>),
                        _featureManager,
                        genericMethod);

                    button.Init(module.name, () => action(module.category));
                    buttons.Add(button);
                }

                groupView.Init(group.Key.ToString(), buttons);
            }
        }
    }
}
