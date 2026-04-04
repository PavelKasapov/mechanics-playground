using MechanicsPlayground.Core;
using NUnit.Framework;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class FloatSettingControlTests
    {
        private FloatSettingControl _control;
        private TextMeshProUGUI _titleText;
        private TextMeshProUGUI _valueText;
        private Slider _slider;

        [SetUp]
        public void Setup()
        {
            (_control, _titleText, _valueText, _slider) = Construct();
        }

        [TearDown]
        public void Teardown()
        {
            if (_control != null)
                GameObject.Destroy(_control.gameObject);
        }

        [Test]
        public void Init_ConfiguresControlCorrectly()
        {
            var floatSettingDescriptor = new FloatSettingDescriptor("TestDescriptor", 10f, 1f, 20f, 1f);
            _control.Init(floatSettingDescriptor);

            Assert.AreEqual("TestDescriptor", _titleText.text);
            Assert.AreEqual((int)Math.Ceiling((floatSettingDescriptor.Max - floatSettingDescriptor.Min) / floatSettingDescriptor.Step), _slider.maxValue);
            Assert.AreEqual((floatSettingDescriptor.Value - floatSettingDescriptor.Min) / floatSettingDescriptor.Step, _slider.value);
            Assert.AreEqual((floatSettingDescriptor.Min + _slider.value * floatSettingDescriptor.Step).ToString("0.##"), _valueText.text);
        }

        [Test]
        public void OnSliderValueChanged_UpdatesValueAndText()
        {
            var floatSettingDescriptor = new FloatSettingDescriptor("TestDescriptor", 10f, 1f, 20f, 1f);
            _control.Init(floatSettingDescriptor);

            var newSliderValue = 12f;
            _slider.onValueChanged.Invoke(newSliderValue);
            Assert.AreEqual((floatSettingDescriptor.Min + newSliderValue * floatSettingDescriptor.Step).ToString("0.##"), _valueText.text);
            Assert.AreEqual(floatSettingDescriptor.Min + newSliderValue * floatSettingDescriptor.Step, floatSettingDescriptor.Value);
        }

        [Test]
        public void Init_WithNullDescriptor_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _control.Init(null));
        }

        [Test]
        public void Init_WithWrongDescriptorType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _control.Init(new NonFloatDescriptor()));
        }

        private (FloatSettingControl, TextMeshProUGUI, TextMeshProUGUI, Slider) Construct()
        {
            var go = new GameObject("FloatSettingControl");
            var floatSettingControl = go.AddComponent<FloatSettingControl>();

            var titleTextGo = new GameObject("TitleTextMesh");
            titleTextGo.transform.SetParent(go.transform);
            var titleTextMesh = titleTextGo.AddComponent<TextMeshProUGUI>();
            floatSettingControl.SetPrivateField("_titleTextMesh", titleTextMesh);

            var valueTextGo = new GameObject("ValueTextMesh");
            valueTextGo.transform.SetParent(go.transform);
            var valueTextMesh = valueTextGo.AddComponent<TextMeshProUGUI>();
            floatSettingControl.SetPrivateField("_valueTextMesh", valueTextMesh);

            var sliderGo = new GameObject("Slider");
            sliderGo.transform.SetParent(go.transform);
            var slider = sliderGo.AddComponent<Slider>();
            floatSettingControl.SetPrivateField("_slider", slider);

            return (floatSettingControl, titleTextMesh, valueTextMesh, slider);
        }
    }

    public class NonFloatDescriptor : ISettingsDescriptor
    {
        public string DisplayName => "TestSettingDescriptor";
    }
}