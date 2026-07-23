using MechanicsPlayground.Core;
using System.Collections.Generic;

namespace MechanicsPlayground.Follow3DCamera
{
    public class RotationSettings : ISettings
    {
        public FloatSettingDescriptor lookSpeedX = new ("Camera Rotation Speed X", 0.18f, 0.01f, 1f, 0.01f);
        public FloatSettingDescriptor lookSpeedY = new("Camera Rotation Speed Y", 0.05f, 0.01f, 1f, 0.01f);
        //public FloatSettingDescriptor lookSmoothTime = new ("Rotation Asseleration/Deseleration Rate", 8f, 4f, 16f, 1f);
        public FloatSettingDescriptor maxPitchAngle = new ("Max Pitch Angle", 85f, 0f, 90f, 1f); //-10 85

        public IEnumerable<ISettingsDescriptor> GetDescriptors()
        {
            yield return lookSpeedX;
            yield return lookSpeedY;
            //yield return lookSmoothTime;
            yield return maxPitchAngle;
        }
    }
}
