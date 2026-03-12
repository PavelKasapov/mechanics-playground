using R3;
using System;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.Camera3D
{
    public class Controller : IInitializable, ITickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly CompositeDisposable _disposables = new();
        private Transform _cameraTransform;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float moveSmoothTime = 10f;

        [Header("Look Settings")]
        [SerializeField] private float lookSpeed = 20f;
        [SerializeField] private float lookSmoothTime = 8f;
        [SerializeField] private float maxPitchAngle = 85f;

        private Vector2 _targetMoveDelta;
        private Vector2 _targetLookDelta;

        private Vector2 _currentMoveDelta;
        private Vector2 _currentLookDelta;

        public Controller (InputAdapter inputAdapter)
        {
            _inputAdapter = inputAdapter;
        }

        public void Dispose() =>_disposables.Dispose();

        public void Initialize()
        {
            _inputAdapter.Move.Subscribe(moveDelta => { _targetMoveDelta = moveDelta; }).AddTo(_disposables);
            _inputAdapter.Look.Subscribe(lookDelta => { _targetLookDelta = lookDelta; }).AddTo(_disposables);
            _cameraTransform = Camera.main.transform;
        }

        public void Tick()
        {
            Debug.Log(_targetLookDelta);
            ProcessMovement();
            ProcessCameraAngle();
        }

        private void ProcessMovement()
        {
            _currentMoveDelta.x = Mathf.Lerp(_currentMoveDelta.x, _targetMoveDelta.x, moveSmoothTime * Time.deltaTime);
            _currentMoveDelta.y = Mathf.Lerp(_currentMoveDelta.y, _targetMoveDelta.y, moveSmoothTime * Time.deltaTime);

            Vector3 forwardMove = _cameraTransform.forward * _currentMoveDelta.y;
            Vector3 rightMove = _cameraTransform.right * _currentMoveDelta.x;
            Vector3 totalMove = (forwardMove + rightMove) * moveSpeed * Time.deltaTime;
            _cameraTransform.position += totalMove;
        }

        private void ProcessCameraAngle()
        {
            _currentLookDelta.x = Mathf.Lerp(_currentLookDelta.x, _targetLookDelta.x, lookSmoothTime * Time.deltaTime);
            _currentLookDelta.y = Mathf.Lerp(_currentLookDelta.y, _targetLookDelta.y, lookSmoothTime * Time.deltaTime);

            float yawAngle = _currentLookDelta.x * lookSpeed * Time.deltaTime;
            _cameraTransform.Rotate(Vector3.up, yawAngle, Space.World);

            float pitchAngle = -_currentLookDelta.y * lookSpeed * Time.deltaTime;
            _cameraTransform.Rotate(Vector3.right, pitchAngle, Space.Self);

            Vector3 currentEuler = _cameraTransform.eulerAngles;
            float currentPitch = currentEuler.x;
            if (currentPitch > 180) currentPitch -= 360;
            currentPitch = Mathf.Clamp(currentPitch, -maxPitchAngle, maxPitchAngle);
            _cameraTransform.eulerAngles = new Vector3(currentPitch, currentEuler.y, 0f);
        }
    }
}