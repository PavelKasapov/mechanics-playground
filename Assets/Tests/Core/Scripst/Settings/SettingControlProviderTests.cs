using MechanicsPlayground.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class SettingControlProviderTests
    {

        private SettingControlProvider _settingControlProvider;
        private FloatSettingControl _floatControlPrefab;
        private Transform _poolTransform;

        [SetUp]
        public void Setup()
        {
            var go = new GameObject("FloatSettingControl");
            _floatControlPrefab = go.AddComponent<FloatSettingControl>();

            var titleTextGo = new GameObject("TitleTextMesh");
            titleTextGo.transform.SetParent(go.transform);
            var titleTextMesh = titleTextGo.AddComponent<TextMeshProUGUI>();
            _floatControlPrefab.SetPrivateField("_titleTextMesh", titleTextMesh);

            var valueTextGo = new GameObject("ValueTextMesh");
            valueTextGo.transform.SetParent(go.transform);
            var valueTextMesh = valueTextGo.AddComponent<TextMeshProUGUI>();
            _floatControlPrefab.SetPrivateField("_valueTextMesh", valueTextMesh);

            var sliderGo = new GameObject("Slider");
            sliderGo.transform.SetParent(go.transform);
            var slider = sliderGo.AddComponent<Slider>();
            _floatControlPrefab.SetPrivateField("_slider", slider);

            _poolTransform = new GameObject("PoolGo").transform;
            var settingPrefabs = new List<BaseSettingControl>() { _floatControlPrefab };
            var resolver = new TestResolver();

            _settingControlProvider = new SettingControlProvider(resolver, settingPrefabs, _poolTransform);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy( _floatControlPrefab );
            GameObject.Destroy( _poolTransform.gameObject );
        }

        [Test]
        public void GetSettingsControl_WithCorrectType_ProvidesCorrectControl()
        {
            var floatSettingsDescriptor = new FloatSettingDescriptor("Test Descrtiptor", 1f, 1f, 5f, 1f);
            var settingsControl = _settingControlProvider.GetSettingControl(floatSettingsDescriptor);

            Assert.IsNotNull(settingsControl);
            Assert.IsInstanceOf<FloatSettingControl>(settingsControl);
        }

        [Test] 
        public void GetSettingsControl_WithNotRegisteredType_ThrowsArgumentException()
        {
            var notRegisteredDescriptor = new NotRegisteredDescriptor();

            Assert.Throws<ArgumentException>(() => _settingControlProvider.GetSettingControl(notRegisteredDescriptor));
        }

        [Test]
        public void ReturnSettingControl_ReleasesControlAndReparents()
        {
            var floatSettingsDescriptor = new FloatSettingDescriptor("Test Descrtiptor", 1f, 1f, 5f, 1f);
            var settingsControl = _settingControlProvider.GetSettingControl(floatSettingsDescriptor);
            _settingControlProvider.ReturnSettingControl(settingsControl);

            Assert.AreEqual(_poolTransform, settingsControl.transform.parent);
        }

    }

    public class NotRegisteredDescriptor : ISettingsDescriptor
    {
        public string DisplayName => throw new NotImplementedException();
    }
}