using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicsPlayground.Core
{
    public class FloatSettingControl : BaseSettingControl
    {
        [SerializeField] private TextMeshProUGUI _titleTextMesh;
        [SerializeField] private TextMeshProUGUI _valueTextMesh;
        [SerializeField] private Slider _slider;

        private FloatSettingDescriptor _descriptor;

        public override Type CorrespondingDescriptorType => typeof(FloatSettingDescriptor);

        public override void Init(ISettingsDescriptor descriptor)
        {
            if (descriptor == null || descriptor is not FloatSettingDescriptor floatSettingDescriptor)
                throw new ArgumentException();

            _descriptor = floatSettingDescriptor;
            _titleTextMesh.text = floatSettingDescriptor.DisplayName;
            _slider.maxValue = (int)Math.Ceiling((floatSettingDescriptor.Max - floatSettingDescriptor.Min) / floatSettingDescriptor.Step);
            _slider.value = (floatSettingDescriptor.Value - floatSettingDescriptor.Min) / floatSettingDescriptor.Step;
            _valueTextMesh.text = (floatSettingDescriptor.Min + _slider.value * floatSettingDescriptor.Step).ToString("0.##");
            _slider.onValueChanged.RemoveAllListeners();
            _slider.onValueChanged.AddListener(OnValueChange);
        }

        private void OnValueChange(float value)
        {
            _valueTextMesh.text = (_descriptor.Min + value * _descriptor.Step).ToString("0.##");
            _descriptor.Value = _descriptor.Min + value * _descriptor.Step;
        }
    }
}
