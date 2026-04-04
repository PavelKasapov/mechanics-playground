using UnityEngine;
using VContainer;

namespace MechanicsPlayground.Tests.Orthographic2DCamera
{
   /* public class MovementHandler
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

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void Tick(Vector2 targetDirectionalDelta, Vector2 cursorPosition, bool isSprinting = false)
        {
            Vector2 moveDelta = targetDirectionalDelta;
            if (moveDelta == Vector2.zero)
            {
                moveDelta = CursorPositionToScreenCorner(cursorPosition);
            }
            Vector3 totalMove = CalcDirectionalMovement(moveDelta);
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

        private Vector2 CursorPositionToScreenCorner(Vector2 cursorPosition)
        {
            return new Vector2(
                cursorPosition.x / Screen.width is var x 
                && x <= _movementSettings.cornerThreshold.Value ? -1 
                : x >= 1 - _movementSettings.cornerThreshold.Value ? 1 
                : 0,

                cursorPosition.y / Screen.height is var y 
                && y <= _movementSettings.cornerThreshold.Value ? -1 
                : y >= 1 - _movementSettings.cornerThreshold.Value ? 1 
                : 0
            );
        }
    }*/
}