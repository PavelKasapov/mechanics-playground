using Unity.Cinemachine;
using UnityEngine;

namespace MechanicsPlayground.Orthographic2DCamera
{
    public class ZoomHandler
    {
        private readonly CinemachineCamera _camera;
        private readonly ZoomSettings _zoomSettings;

        private float _targetOrthographicSize;
        private float _zoomVelocity;

        public ZoomHandler(CinemachineCamera camera, ZoomSettings zoomSettings)
        {
            _camera = camera;
            _zoomSettings = zoomSettings;
            _targetOrthographicSize = _camera.Lens.OrthographicSize;
        }

        public void Tick(float inputZoomingDelta)
        {
            _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize - inputZoomingDelta * _zoomSettings.zoomSpeed.Value, _zoomSettings.minOrthographicSize.Value, _zoomSettings.maxOrthographicSize.Value);
            
            float currentFOV = _camera.Lens.OrthographicSize;

            if (Mathf.Approximately(currentFOV, _targetOrthographicSize))
            {
                return;
            }

            _camera.Lens.OrthographicSize = Mathf.SmoothDamp(_camera.Lens.OrthographicSize, _targetOrthographicSize, ref _zoomVelocity, _zoomSettings.smoothTime.Value);
        }
    }
}