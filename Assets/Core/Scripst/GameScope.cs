using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] FloatSettingControl _floatControlPrefab;
        [SerializeField] SettingsControlModule _modulePrefab;
        [SerializeField] Transform _uISettingsPanel;
        [SerializeField] Transform _poolTransform;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(Camera.main.transform).Keyed("CameraTransform");
            builder.RegisterInstance(Camera.main);
            builder.RegisterInstance(_uISettingsPanel).Keyed("UISettingsPanel");
            builder.RegisterInstance(_poolTransform).Keyed("SettingControlPool");

            builder.RegisterInstance(_floatControlPrefab).As<BaseSettingControl>().AsSelf();
            builder.RegisterInstance(_modulePrefab);

            builder.Register<SettingControlProvider>(Lifetime.Singleton);

            builder.Register<FeatureRegistry>(Lifetime.Singleton)
                .As<IFeatureRegistry>()
                .As<IFeatureRegistryWritter>();

            builder.RegisterEntryPoint<FeatureManager>();
            builder.Register<SettingsRegistry>(Lifetime.Singleton);

            builder.RegisterEntryPoint<UISettingsPanelPresenter>(Lifetime.Singleton);
        }
    }
}
