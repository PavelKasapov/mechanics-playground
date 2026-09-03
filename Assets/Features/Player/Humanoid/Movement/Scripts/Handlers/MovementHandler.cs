using MechanicsPlayground.Core;
using Unity.Cinemachine;
using UnityEngine;

namespace MechanicsPlayground.HumanoidMovement
{
    public class MovementHandler
    {
        private const float Cos45 = 0.707f;
        private const float AccelerationInAirModifier = 0.4f;
        private readonly Vector3 _planeNormal = Vector3.up;

        private readonly CameraHandler _cameraHandler;
        private readonly MovementSettings _movementSettings;
        private readonly GroundedProvider _groundedProvider;
        private readonly Rigidbody _rigidbody;
        private readonly CharacterController _characterController;

        private float _verticalVelocity;
        private Vector3 _horizontalMove;

        public MovementHandler(
            CameraHandler cameraHandler, 
            MovementSettings movementSettings,
            GroundedProvider groundedProvider,
            Rigidbody rigidbody,
            CharacterController characterController)
        {
            _cameraHandler = cameraHandler;
            _movementSettings = movementSettings;
            _groundedProvider = groundedProvider;
            _rigidbody = rigidbody;
            _characterController = characterController;
        }

        public void FixedTick(Vector2 input, bool isSprinting = false)
        {
            if (input == Vector2.zero) 
            {
                _horizontalMove = Deceleration(0);
            }
            else
            {
                var accelerationDirection = CalcAccelerationDirection(input);
                _horizontalMove = Acceleration(accelerationDirection, isSprinting);
            }

            if (_characterController.isGrounded && _verticalVelocity < 0)
                _verticalVelocity = -2f;
            else
                _verticalVelocity += Physics.gravity.y * Time.deltaTime;

            Debug.Log($"_characterController.isGrounded:{_characterController.isGrounded} _groundedProvider.IsGrounded:{_groundedProvider.IsGrounded}");

            Vector3 motion = (_horizontalMove + Vector3.up * _verticalVelocity) * Time.deltaTime;
            _characterController.Move(motion);
        }

        private Vector3 Acceleration(Vector3 accelerationDirection, bool isSprinting)
        {
            var acceleration = accelerationDirection * _movementSettings.accelerationRate.Value * (_characterController.isGrounded ? 1 : AccelerationInAirModifier) * Time.fixedDeltaTime;
            var velocity = _characterController.velocity;
            velocity.y = 0;
            velocity += acceleration;

            var targetSpeed = (isSprinting && _characterController.isGrounded) 
                ? _movementSettings.maxMoveSpeed.Value * _movementSettings.sprintMultiplier.Value 
                : _movementSettings.maxMoveSpeed.Value;

            targetSpeed = _characterController.isGrounded ? targetSpeed : targetSpeed * 0.4f; //TODO
            
            if (velocity.sqrMagnitude > Mathf.Pow(targetSpeed, 2))
            {
                return Deceleration(targetSpeed);
            }
            else
            {
                return velocity;
            }
        }

        private Vector3 Deceleration(float targetSpeed)
        {
            var velocity = _characterController.velocity;
            velocity.y = 0;
            var tickDeceleration = _movementSettings.accelerationRate.Value * Time.fixedDeltaTime;
            tickDeceleration = _characterController.isGrounded ? tickDeceleration : tickDeceleration * AccelerationInAirModifier; //TODO

            if (velocity.sqrMagnitude > Mathf.Pow(targetSpeed - tickDeceleration, 2))
            {
                velocity -= velocity.normalized * tickDeceleration;
            }
            else
            {
                velocity.Normalize();
                velocity *= targetSpeed;
            }
            return velocity;
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

            Vector3 direction = /*Vector3.ProjectOnPlane(*/forward * input.y/*, _groundedProvider.GroundNormal)*/ + /*Vector3.ProjectOnPlane(*/right * input.x/*, _groundedProvider.GroundNormal)*/;

            return direction;
        }

        public void JumpAction()
        {
            if (_characterController.isGrounded)
                _verticalVelocity = Mathf.Sqrt(_movementSettings.jumpHeight.Value * -2f * Physics.gravity.y);
        }
    }
}