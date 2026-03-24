using UnityEngine;
using VContainer;

namespace MechanicsPlayground.Orthographic2DCamera
{
    public class MovementHandler
    {
        private readonly Transform _cameraTransform;
        private readonly Camera _camera;
        private readonly MovementSettings _movementSettings;

        private Vector2 _directionalMovementDelta;

        public MovementHandler([Key("CameraTransform")]Transform cameraTransform, Camera camera, MovementSettings movementSettings )
        {
            _cameraTransform = cameraTransform;
            _camera = camera;
            _movementSettings = movementSettings;
        }

        public void Tick(Vector2 targetDirectionalDelta, float targetVerticalDelta, bool isSprinting = false)
        {
            Vector3 totalMove = CalcDirectionalMovement(targetDirectionalDelta);
            totalMove *= (isSprinting ? _movementSettings.sprintMultiplier.Value : 1);
            _cameraTransform.position += totalMove;
        }

        private Vector3 CalcDirectionalMovement(Vector2 targetDirectionalDelta)
        {
            _directionalMovementDelta = Vector2.Lerp(_directionalMovementDelta, targetDirectionalDelta, _movementSettings.smoothTime.Value * Time.deltaTime);

            Vector3 forwardMove = Vector3.forward * _directionalMovementDelta.y;
            Vector3 rightMove = Vector3.right * _directionalMovementDelta.x;
            return _movementSettings.moveSpeed.Value * _camera.orthographicSize * Time.deltaTime * (forwardMove + rightMove);
        }
    }
}