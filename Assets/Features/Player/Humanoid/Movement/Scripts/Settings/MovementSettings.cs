using MechanicsPlayground.Core;
using System.Collections.Generic;

namespace MechanicsPlayground.HumanoidMovement
{
    public class MovementSettings : ISettings
    {
        public FloatSettingDescriptor maxMoveSpeed = new("Movement Speed", 25f, 0.1f, 40f, 0.1f);
        public FloatSettingDescriptor accelerationRate = new ("Asseleration/Deseleration Rate", 50f, 10f, 100f, 5f);
        public FloatSettingDescriptor sprintMultiplier = new("Sprint Multiplier", 1.5f, 0.5f, 2.5f, 0.5f);
        public FloatSettingDescriptor jumpForce = new("JumpForce", 6.5f, 1f, 15f, 0.5f);

        public IEnumerable<ISettingsDescriptor> GetDescriptors()
        {
            yield return maxMoveSpeed;
            yield return accelerationRate;
            yield return sprintMultiplier;
            yield return jumpForce;
        }
    }
}