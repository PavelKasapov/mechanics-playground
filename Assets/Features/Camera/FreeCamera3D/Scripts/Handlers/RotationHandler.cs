using UnityEngine;
using VContainer;

namespace MechanicsPlayground.FreeCamera3D
{
    public class RotationHandler
    {
        private readonly Transform _cameraTransform;
        private readonly RotationSettings _rotationSettings;

        private Vector2 _currentLookDelta;
        private bool _lastCursorVisibility;
        public RotationHandler([Key("CameraTransform")] Transform cameraTransform, RotationSettings rotationSettings)
        {
            _cameraTransform = cameraTransform;
            _rotationSettings = rotationSettings;
            Cursor.visible = false;
        }

        public void Tick(Vector2 targetLookDelta, bool cursorVisibility)
        {
            if (_lastCursorVisibility != cursorVisibility)
            {
                Cursor.visible = cursorVisibility;
                Cursor.lockState = cursorVisibility ? CursorLockMode.None : CursorLockMode.Locked;
                _lastCursorVisibility = cursorVisibility;
            }

            _currentLookDelta.x = Mathf.Lerp(_currentLookDelta.x, cursorVisibility ? 0 : targetLookDelta.x, _rotationSettings.lookSmoothTime.Value * Time.deltaTime);
            _currentLookDelta.y = Mathf.Lerp(_currentLookDelta.y, cursorVisibility ? 0 : targetLookDelta.y, _rotationSettings.lookSmoothTime.Value * Time.deltaTime);

            Vector3 currentEuler = _cameraTransform.eulerAngles;

            float yawAngleDelta = _currentLookDelta.x * _rotationSettings.lookSpeed.Value * Time.deltaTime;
            float pitchAngleDelta = -_currentLookDelta.y * _rotationSettings.lookSpeed.Value * Time.deltaTime;

            float newYawAngle = currentEuler.y + yawAngleDelta;
            float currentPitch = currentEuler.x > 180 ? currentEuler.x - 360 : currentEuler.x;
            float newPitchAngle = Mathf.Clamp(currentPitch + pitchAngleDelta, -_rotationSettings.maxPitchAngle.Value, _rotationSettings.maxPitchAngle.Value);

            _cameraTransform.rotation = Quaternion.Euler(newPitchAngle, newYawAngle, 0f);
        }
    }
}