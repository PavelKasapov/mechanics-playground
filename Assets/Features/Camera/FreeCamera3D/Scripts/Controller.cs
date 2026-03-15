using R3;
using System;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.FreeCamera3D
{
    public class Controller : IInitializable, ITickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly MovementHandler _movementHandler;
        private readonly RotationHandler _rotationHandler;
        private readonly ZoomHandler _zoomHandler;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _inputMoveDelta;
        private float _inputVerticalDelta;
        private Vector2 _inputLookDelta;
        private bool _isSprinting;
        private bool _isZooming;

        public Controller (InputAdapter inputAdapter, MovementHandler movementHandler, RotationHandler rotationHandler, ZoomHandler zoomHandler)
        {
            _inputAdapter = inputAdapter;
            _movementHandler = movementHandler;
            _rotationHandler = rotationHandler;
            _zoomHandler = zoomHandler;
        }

        public void Dispose() =>_disposables.Dispose();

        public void Initialize()
        {
            _inputAdapter.Move.Subscribe(moveDelta => { _inputMoveDelta = moveDelta; }).AddTo(_disposables);
            _inputAdapter.Look.Subscribe(lookDelta => { _inputLookDelta = lookDelta; }).AddTo(_disposables);
            _inputAdapter.VerticalMomement.Subscribe(vervicalDelta => { _inputVerticalDelta = vervicalDelta; }).AddTo(_disposables);
            _inputAdapter.Sprint.Subscribe(isSprinting => { _isSprinting = isSprinting; }).AddTo(_disposables);
            _inputAdapter.Zoom.Subscribe(isZooming => { _isZooming = isZooming; }).AddTo(_disposables);
        }

        public void Tick()
        {
            _rotationHandler.Tick(_inputLookDelta);
            _movementHandler.Tick(_inputMoveDelta, _inputVerticalDelta, _isSprinting);
            _zoomHandler.Tick(_isZooming);
        }
    }
}