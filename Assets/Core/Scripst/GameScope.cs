using System;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Transform _uISettingsPanel;
        [SerializeField] private Transform _featureModulesPanel;
        [SerializeField] private Transform _poolTransform;
        [SerializeField] private HelpWindow _helpWindow;
        [SerializeField] private SettingsControlModule _modulePrefab;
        [SerializeField] private FeatureModuleGroup _featureGroupPrefab;
        [SerializeField] private FeatureButton _featureButtonPrefab;
        [SerializeField] private FeatureButton _featureHelpButtonPrefab;
        [SerializeField] private FloatSettingControl _floatControlPrefab;
        [SerializeField] private CinemachineCamera _cinemachineMockCamera;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(Camera.main.transform).Keyed("CameraTransform");
            builder.RegisterInstance(Camera.main);

            builder.RegisterInstance(_helpWindow);
            builder.RegisterInstance(_poolTransform).Keyed("SettingControlPool");
            builder.RegisterInstance(_featureModulesPanel).Keyed("FeatureModulesPanel");
            builder.RegisterInstance(_uISettingsPanel).Keyed("UISettingsPanel");
            builder.RegisterInstance(_cinemachineMockCamera).Keyed("MockCamera");

            builder.RegisterMonobehaviourFactory<SettingsControlModule>(_modulePrefab);
            builder.RegisterMonobehaviourFactory<FeatureModuleGroup>(_featureGroupPrefab);
            builder.RegisterMonobehaviourFactory<FeatureButton>(_featureButtonPrefab);
            builder.RegisterMonobehaviourFactory<FeatureButton>(_featureHelpButtonPrefab).Keyed("HelpButtonFactory");

            builder.RegisterInstance(_floatControlPrefab).As<BaseSettingControl>();

            builder.Register<SettingControlProvider>(Lifetime.Singleton);
            builder.Register<SettingsRegistry>(Lifetime.Singleton);
            builder.Register<CameraHandler>(Lifetime.Singleton);
            builder.Register<PlayerPivotHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.RegisterEntryPoint<FeatureManager>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<UISettingsPanelPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FeatureModulesPanelPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<HelpWindowPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PerspectiveToOrthoCustomBlender>(Lifetime.Singleton);

            builder.Register<FeatureRegistry>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }

    public static class VContainerExtensions
    {
        public static RegistrationBuilder RegisterMonobehaviourFactory<T>(this IContainerBuilder builder, T prefab, Lifetime lifetime = Lifetime.Singleton)
            where T : MonoBehaviour
        {
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));
            return builder.Register(container => new SimpleMonobehaviourFactory<T>(container, prefab), lifetime);
        }
    }
}
