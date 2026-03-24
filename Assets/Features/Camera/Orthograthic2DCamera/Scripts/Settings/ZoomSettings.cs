using MechanicsPlayground.Core;
using System.Collections.Generic;

namespace MechanicsPlayground.Orthographic2DCamera
{
    public class ZoomSettings : ISettings
    {
        public FloatSettingDescriptor minOrthographicSize = new ("Min Orthographic Size", 10f, 5f, 20f, 1f);
        public FloatSettingDescriptor maxOrthographicSize = new ("Max Orthographic Size", 60f, 20f, 90f, 1f);
        public FloatSettingDescriptor zoomSpeed = new("Zoom Speed", 5f, 1f, 10f, 0.5f);
        public FloatSettingDescriptor smoothTime = new ("Smooth Time", 0.2f, 0.1f, 0.4f, 0.05f);

        public IEnumerable<ISettingsDescriptor> GetDescriptors()
        {
            yield return minOrthographicSize;
            yield return maxOrthographicSize;
            yield return zoomSpeed;
            yield return smoothTime;
        }
    }
}
