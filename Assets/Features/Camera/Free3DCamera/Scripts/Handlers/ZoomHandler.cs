using UnityEngine;

namespace MechanicsPlayground.Free3DCamera
{
    public class ZoomHandler
    {
        private readonly Camera _camera;
        private readonly ZoomSettings _zoomSettings;
        private readonly float _baseFOV;

        //private float _zoomDelta;
        private float _zoomVelocity;

        public ZoomHandler(Camera camera, ZoomSettings zoomSettings)
        {
            _camera = camera;
            _zoomSettings = zoomSettings;
            _baseFOV = _camera.fieldOfView;
        }

        public void Tick(bool isZooming)
        {
            float targetFOV = isZooming ? _baseFOV / _zoomSettings.zoomMultiplier.Value : _baseFOV;
            float currentFOV = _camera.fieldOfView;

            if (Mathf.Approximately(currentFOV, targetFOV))
            {
                return;
            }

            _camera.fieldOfView = Mathf.SmoothDamp(_camera.fieldOfView, targetFOV, ref _zoomVelocity, _zoomSettings.smoothTime.Value);
        }
    }
}