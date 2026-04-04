using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Tests.Core
{
   /* public class GameScope : LifetimeScope
    {
        [SerializeField] FloatSettingControlTests _floatControlPrefab;
        [SerializeField] SettingsControlModuleTests _modulePrefab;
        [SerializeField] Transform _uISettingsPanel;
        [SerializeField] FeatureModuleGroupTests _featureGroupPrefab;
        [SerializeField] FeatureButtonTests _featureButtonPrefab;
        [SerializeField] Transform _featureModulesPanel;
        [SerializeField] Transform _poolTransform;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(Camera.main.transform).Keyed("CameraTransform");
            builder.RegisterInstance(Camera.main);

            builder.RegisterInstance(_poolTransform).Keyed("SettingControlPool"); 

            builder.RegisterInstance(_uISettingsPanel).Keyed("UISettingsPanel");
            builder.RegisterInstance(_modulePrefab);
            builder.RegisterInstance(_floatControlPrefab).As<BaseSettingControl>().AsSelf();

            builder.RegisterInstance(_featureModulesPanel).Keyed("FeatureModulesPanel");
            builder.RegisterInstance(_featureGroupPrefab);
            builder.RegisterInstance(_featureButtonPrefab);


            builder.Register<SettingControlProviderTests>(Lifetime.Singleton);

            builder.Register<FeatureRegistry>(Lifetime.Singleton)
                .As<IFeatureRegistry>()
                .As<IFeatureRegistryWritter>();

            builder.RegisterEntryPoint<FeatureManagerTests>(Lifetime.Singleton).AsSelf();
            builder.Register<SettingsRegistry>(Lifetime.Singleton);

            builder.RegisterEntryPoint<UISettingsPanelPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FeatureModulesPanelPresenterTests>(Lifetime.Singleton);
        }
    }*/
}
