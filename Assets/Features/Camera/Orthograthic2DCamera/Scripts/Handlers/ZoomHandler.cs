using UnityEngine;

namespace MechanicsPlayground.Orthographic2DCamera
{
    public class ZoomHandler
    {
        private readonly Camera _camera;
        private readonly ZoomSettings _zoomSettings;

        private float _targetOrthographicSize;
        private float _zoomVelocity;

        public ZoomHandler(Camera camera, ZoomSettings zoomSettings)
        {
            _camera = camera;
            _zoomSettings = zoomSettings;
            _targetOrthographicSize = _camera.orthographicSize;
        }

        public void Tick(float inputZoomingDelta)
        {
            _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize - inputZoomingDelta * _zoomSettings.zoomSpeed.Value, _zoomSettings.minOrthographicSize.Value, _zoomSettings.maxOrthographicSize.Value);
            
            float currentFOV = _camera.orthographicSize;

            if (Mathf.Approximately(currentFOV, _targetOrthographicSize))
            {
                return;
            }

            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _targetOrthographicSize, ref _zoomVelocity, _zoomSettings.smoothTime.Value);
        }
    }
}