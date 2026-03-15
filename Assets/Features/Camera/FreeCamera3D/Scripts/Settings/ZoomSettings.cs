using System;

namespace MechanicsPlayground.FreeCamera3D
{
    [Serializable]
    public class ZoomSettings
    {
        public float zoomMultiplier = 3f;
        public float zoomChangingSpeed = 1f;
        public float smoothTime = 0.2f;
    }
}
