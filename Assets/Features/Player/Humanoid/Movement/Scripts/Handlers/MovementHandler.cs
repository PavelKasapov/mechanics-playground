using MechanicsPlayground.Core;
using Unity.Cinemachine;
using UnityEngine;

namespace MechanicsPlayground.HumanoidMovement
{
    public class MovementHandler
    {
        private const float Cos45 = 0.707f;
        private readonly Vector3 _planeNormal = Vector3.up;

        private readonly CameraHandler _cameraHandler;
        private readonly MovementSettings _movementSettings;
        private readonly GroundedProvider _groundedProvider;
        private readonly Rigidbody _rigidbody;

        public MovementHandler(
            CameraHandler cameraHandler, 
            MovementSettings movementSettings,
            GroundedProvider groundedProvider,
            Rigidbody rigidbody)
        {
            _cameraHandler = cameraHandler;
            _movementSettings = movementSettings;
            _groundedProvider = groundedProvider;
            _rigidbody = rigidbody;
        }

        public void FixedTick(Vector2 input, bool sprintMultiplier = false)
        {
            if (input == Vector2.zero) 
            {
                Deceleration(0);
                return;
            }

            var accelerationDirection = CalcAccelerationDirection(input);

            Acceleration(accelerationDirection, sprintMultiplier);
        }

        private void Acceleration(Vector3 accelerationDirection, bool isSprinting)
        {
            var velocity = _rigidbody.linearVelocity + accelerationDirection * _movementSettings.accelerationRate.Value * Time.fixedDeltaTime;
            var targetSpeed = isSprinting ? _movementSettings.maxMoveSpeed.Value * _movementSettings.sprintMultiplier.Value : _movementSettings.maxMoveSpeed.Value;
            if (velocity.sqrMagnitude > Mathf.Pow(targetSpeed, 2))
            {
                Deceleration(targetSpeed);
            }
            else
            {
                _rigidbody.linearVelocity = velocity;
            }
        }

        private void Deceleration(float targetSpeed)
        {
            var velocity = _rigidbody.linearVelocity;
            var verticalVelocity = velocity.y;
            velocity.y = 0;

            var tickDeceleration = _movementSettings.accelerationRate.Value * Time.fixedDeltaTime;
            if (velocity.sqrMagnitude > Mathf.Pow(targetSpeed - tickDeceleration, 2))
            {
                velocity -= velocity.normalized * tickDeceleration;
            }
            else
            {
                velocity.Normalize();
                velocity *= targetSpeed;
            }
            velocity += Vector3.up * verticalVelocity;
            _rigidbody.linearVelocity = velocity;
        }

        private Vector3 CalcAccelerationDirection(Vector2 input)
        {
            Quaternion cameraOrientation = _cameraHandler.CameraFacade != null
                ? _cameraHandler.CameraFacade.CinemachineCamera.State.GetFinalOrientation()
                : Quaternion.identity;

            Vector3 camForward = cameraOrientation * Vector3.forward;
            float dot = Mathf.Abs(Vector3.Dot(camForward, _planeNormal));

            bool isVertical = dot > Cos45;
            Vector3 baseDir = isVertical ? cameraOrientation * Vector3.up : camForward;

            Vector3 forward = baseDir - _planeNormal * Vector3.Dot(baseDir, _planeNormal);
            forward.Normalize();

            Vector3 right = Vector3.Cross(_planeNormal, forward).normalized;

            Vector3 direction = forward * input.y + right * input.x;

            return direction;
        }

        public void JumpAction()
        {
            if (_groundedProvider.IsGrounded)
                _rigidbody.AddForce(Vector3.up * _movementSettings.jumpForce.Value, ForceMode.Impulse);
        }
    }
}