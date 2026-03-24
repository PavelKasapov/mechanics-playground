using MechanicsPlayground.Core;
using System.Collections.Generic;

namespace MechanicsPlayground.Orthographic2DCamera
{
    public class MovementSettings : ISettings
    {
        public FloatSettingDescriptor moveSpeed = new("Movement Speed", 2f, 0.1f, 4f, 0.1f);
        public FloatSettingDescriptor smoothTime = new ("Asseleration/Deseleration Rate", 6f, 1f, 15f, 1f);
        public FloatSettingDescriptor sprintMultiplier = new("Sprint Multiplier", 1.5f, 0.5f, 2.5f, 0.5f);

        public IEnumerable<ISettingsDescriptor> GetDescriptors()
        {
            yield return moveSpeed;
            yield return smoothTime;
            yield return sprintMultiplier;
        }
    }
}