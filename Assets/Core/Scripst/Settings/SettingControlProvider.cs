using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace MechanicsPlayground.Core
{
    public class SettingControlProvider 
    {
        private readonly IObjectResolver _resolver;
        private readonly Dictionary<Type, IObjectPool<BaseSettingControl>> _pools = new();
        private readonly Transform _poolTransform;

        public SettingControlProvider(IObjectResolver resolver, IEnumerable<BaseSettingControl> settingPrefabs, [Key("SettingControlPool")]Transform poolTransform)
        {
            _resolver = resolver;
            _poolTransform = poolTransform;
            foreach (var prefab in settingPrefabs) 
            {
                Register(prefab);
            }
        }

        public void Register<TControl>(TControl controlPrefab)
            where TControl : BaseSettingControl
        {
            var factory = new SimpleMonobehaviourFactory<TControl>(_resolver, controlPrefab);
       
            var pool = new ObjectPool<TControl>(
                () => factory.Create(),
                control => control.gameObject.SetActive(true),
                control => control.gameObject.SetActive(false),
                defaultCapacity: 5);

            _pools.Add(controlPrefab.CorrespondingDescriptorType, (IObjectPool<BaseSettingControl>)pool);
        }

        public BaseSettingControl GetSettingControl(ISettingsDescriptor settingsDescriptor)
        {
            var type = settingsDescriptor.GetType();

            if (!_pools.TryGetValue(type, out var pool))
                throw new ArgumentException($"No pool for descriptor type {type}");
            
            var control = pool.Get();
            control.Init(settingsDescriptor);
            return control;
        }

        public void ReturnSettingControl(BaseSettingControl settingControl)
        {
            var type = settingControl.CorrespondingDescriptorType;

            if (!_pools.TryGetValue(type, out var pool))
                throw new ArgumentException($"No pool for descriptor type {type}");

            settingControl.transform.SetParent(_poolTransform);
            pool.Release(settingControl);
        }
    }
}
