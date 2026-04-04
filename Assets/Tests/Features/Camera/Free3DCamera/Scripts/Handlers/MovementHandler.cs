using UnityEngine;
using VContainer;

namespace MechanicsPlayground.Tests.Free3DCamera
{
    /*public class MovementHandler
    {
        private readonly Transform _cameraTransform;
        private readonly MovementSettings _movementSettings;

        private Vector2 _directionalMovementDelta;
        private float _verticalMovementDelta;

        public MovementHandler([Key("CameraTransform")]Transform cameraTransform, MovementSettings movementSettings )
        {
            _cameraTransform = cameraTransform;
            _movementSettings = movementSettings;
        }

        public void Tick(Vector2 targetDirectionalDelta, float targetVerticalDelta, bool isSprinting = false)
        {
            Vector3 totalMove = CalcDirectionalMovement(targetDirectionalDelta);
            totalMove.y += CalcVerticalMovement(targetVerticalDelta);
            totalMove *= (isSprinting ? _movementSettings.sprintMultiplier.Value : 1);
            _cameraTransform.position += totalMove;
        }

        private Vector3 CalcDirectionalMovement(Vector2 targetDirectionalDelta)
        {
            _directionalMovementDelta = Vector2.Lerp(_directionalMovementDelta, targetDirectionalDelta, _movementSettings.smoothTime.Value * Time.deltaTime);

            Vector3 forwardMove = _cameraTransform.forward * _directionalMovementDelta.y;
            Vector3 rightMove = _cameraTransform.right * _directionalMovementDelta.x;
            return _movementSettings.moveSpeed.Value * Time.deltaTime * (forwardMove + rightMove);
        }

        private float CalcVerticalMovement(float targetVerticalDelta)
        {
            _verticalMovementDelta = Mathf.Lerp(_verticalMovementDelta, targetVerticalDelta, _movementSettings.smoothTime.Value * Time.deltaTime);

            return  _movementSettings.moveSpeed.Value * Time.deltaTime * _verticalMovementDelta;
        }
    }*/
}