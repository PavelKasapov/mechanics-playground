using Cysharp.Threading.Tasks;
using MechanicsPlayground.Core;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using R3;
using System;

namespace MechanicsPlayground.Follow3DCamera
{
    public class RotationHandler : IDisposable
    {
        private readonly RotationSettings _rotationSettings;
        private readonly IReadOnlyPlayerPivotHandler _playerPivotHandler;
        private readonly CinemachineCamera _cinemachineCamera;
        private readonly CinemachineOrbitalFollow _orbitalFollow;
        private readonly CompositeDisposable _disposables = new();

        private float _yaw; 
        private float _pitch;
        
        public RotationHandler(IReadOnlyPlayerPivotHandler playerPivotHandler, CinemachineCamera cinemachineCamera, RotationSettings rotationSettings)
        {
            _playerPivotHandler = playerPivotHandler;
            _cinemachineCamera = cinemachineCamera;
            _rotationSettings = rotationSettings;
            _orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();

            _playerPivotHandler.PivotTransform
                .Subscribe(pivot =>
                {
                    _cinemachineCamera.Follow = pivot;

                    /*if (pivot != null)
                    {
                       var euler = pivot.rotation.eulerAngles;
                       _yaw = euler.y;
                       _pitch = euler.x;
                    }*/
                })
                .AddTo(_disposables);
        }

        public void Tick(Vector2 delta, bool cursorVisibility)
        {
            if (_cinemachineCamera.Follow == null)
                return;

            _orbitalFollow.HorizontalAxis.Value += delta.x * _rotationSettings.lookSpeedX.Value;
            _orbitalFollow.VerticalAxis.Value += delta.y * _rotationSettings.lookSpeedY.Value;
            _orbitalFollow.VerticalAxis.Value = Mathf.Clamp(_orbitalFollow.VerticalAxis.Value, -_rotationSettings.maxPitchAngle.Value, _rotationSettings.maxPitchAngle.Value);

           /* _yaw += delta.x * _rotationSettings.lookSpeed.Value;
            _pitch -= delta.y * _rotationSettings.lookSpeed.Value;
            _pitch = Mathf.Clamp(_pitch, -_rotationSettings.maxPitchAngle.Value, _rotationSettings.maxPitchAngle.Value);

            _playerPivotHandler.PivotTransform.CurrentValue.rotation = Quaternion.Euler(_pitch, _yaw, 0f);*/
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}