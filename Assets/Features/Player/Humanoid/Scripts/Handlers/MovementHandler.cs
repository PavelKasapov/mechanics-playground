using MechanicsPlayground.Core;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace MechanicsPlayground.HumanoidPlayer
{
    public class MovementHandler
    {
        private const float Cos45 = 0.707f;
        private readonly Vector3 _planeNormal = Vector3.up;

        private readonly Transform _playerTransform;
        private readonly CameraHandler _cameraHandler;
        private readonly MovementSettings _movementSettings;
        

        public MovementHandler([Key("PlayerTransform")] Transform playerTransform, CameraHandler cameraHandler, MovementSettings movementSettings)
        {
            _playerTransform = playerTransform;
            _cameraHandler = cameraHandler;
            _movementSettings = movementSettings;
        }

        public void Tick(Vector2 input, bool isSprinting = false)
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

            Vector3 move = forward * input.y + right * input.x;

            _playerTransform.position += move * _movementSettings.moveSpeed.Value * _movementSettings.sprintMultiplier.Value * Time.deltaTime;
        }
    }
}