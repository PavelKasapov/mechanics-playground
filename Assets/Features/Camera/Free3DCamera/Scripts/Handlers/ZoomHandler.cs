using Unity.Cinemachine;
using UnityEngine;

namespace MechanicsPlayground.Free3DCamera
{
    public class ZoomHandler
    {
        private readonly CinemachineCamera _camera;
        private readonly ZoomSettings _zoomSettings;
        private readonly float _baseFOV;

        //private float _zoomDelta;
        private float _zoomVelocity;

        public ZoomHandler(CinemachineCamera camera, ZoomSettings zoomSettings)
        {
            _camera = camera;
            _zoomSettings = zoomSettings;
            _baseFOV = _camera.Lens.FieldOfView;
        }

        public void Tick(bool isZooming)
        {
            float targetFOV = isZooming ? _baseFOV / _zoomSettings.zoomMultiplier.Value : _baseFOV;
            float currentFOV = _camera.Lens.FieldOfView;

            if (Mathf.Approximately(currentFOV, targetFOV))
            {
                return;
            }

            _camera.Lens.FieldOfView = Mathf.SmoothDamp(_camera.Lens.FieldOfView, targetFOV, ref _zoomVelocity, _zoomSettings.smoothTime.Value);
        }
    }
}