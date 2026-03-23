using System;
using UnityEngine;

namespace MechanicsPlayground.Core
{
    public abstract class BaseSettingControl : MonoBehaviour
    {
        public abstract Type CorrespondingDescriptorType { get; }
        public abstract void Init(ISettingsDescriptor descriptor);
    }
}
