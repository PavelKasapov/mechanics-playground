using MechanicsPlayground.Core;
using System.Collections.Generic;

namespace MechanicsPlayground.Free3DCamera
{
    public class ZoomSettings : ISettings
    {
        public FloatSettingDescriptor zoomMultiplier = new ("Zoom Multiplayer", 3f, 1.5f, 6f, 0.5f);
        public FloatSettingDescriptor zoomChangingSpeed = new ("Zoom Changing Speed", 1f, 0.5f, 2f, 0.1f);
        public FloatSettingDescriptor smoothTime = new ("Smooth Time", 0.2f, 0.1f, 0.4f, 0.05f);

        public IEnumerable<ISettingsDescriptor> GetDescriptors()
        {
            yield return zoomMultiplier;
            yield return zoomChangingSpeed;
            yield return smoothTime;
        }
    }
}
