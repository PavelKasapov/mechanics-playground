using UnityEngine;

namespace MechanicsPlayground.Core
{
    public class FloatSettingDescriptor : ISettingsDescriptor
    {
        public string DisplayName { get; }
        public float Value { get; set; }
        public float Min { get; }
        public float Max { get; }
        public float Step { get; }

        public FloatSettingDescriptor(string displayName, float value, float min, float max, float step)
        {
            DisplayName = displayName;
            Value = Mathf.Clamp(value, min, max);
            Min = min;
            Max = max;
            Step = step;
        }
    }
}