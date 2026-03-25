using MechanicsPlayground.Core;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using System.Linq;

namespace MechanicsPlayground.Free3DCamera
{
    public class Controller : IInitializable, ITickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly MovementHandler _movementHandler;
        private readonly RotationHandler _rotationHandler;
        private readonly ZoomHandler _zoomHandler;
        private readonly SettingsRegistry _settingsRegistry;
        private readonly IEnumerable<ISettings> _settings;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _inputLookDelta;
        private Vector2 _inputMoveDelta;
        private float _inputVerticalDelta;
        private bool _isSprinting;
        private bool _isZooming;
        private bool _isCursorVisiblile;

        public Controller (InputAdapter inputAdapter, MovementHandler movementHandler, RotationHandler rotationHandler, ZoomHandler zoomHandler, SettingsRegistry settingsRegistry ,IEnumerable<ISettings> settings)
        {
            _inputAdapter = inputAdapter;
            _movementHandler = movementHandler;
            _rotationHandler = rotationHandler;
            _zoomHandler = zoomHandler;
            _settingsRegistry = settingsRegistry;
            _settings = settings;
        }

        public void Dispose() =>_disposables.Dispose();

        public void Initialize()
        {
            _inputAdapter.Move.Subscribe(moveDelta => { _inputMoveDelta = moveDelta; }).AddTo(_disposables);
            _inputAdapter.Look.Subscribe(lookDelta => { _inputLookDelta = lookDelta; }).AddTo(_disposables);
            _inputAdapter.VerticalMovement.Subscribe(vervicalDelta => { _inputVerticalDelta = vervicalDelta; }).AddTo(_disposables);
            _inputAdapter.Sprint.Subscribe(isSprinting => { _isSprinting = isSprinting; }).AddTo(_disposables);
            _inputAdapter.Zoom.Subscribe(isZooming => { _isZooming = isZooming; }).AddTo(_disposables);
            _inputAdapter.CursorVisiblility.Subscribe(isCursorVisiblile => { _isCursorVisiblile = isCursorVisiblile; }).AddTo(_disposables);

            _settingsRegistry.RegisterModule("Free3DCamera", _settings.SelectMany(s => s.GetDescriptors()).ToList()).AddTo(_disposables);
        }

        public void Tick()
        {
            _rotationHandler.Tick(_inputLookDelta, _isCursorVisiblile);
            _movementHandler.Tick(_inputMoveDelta, _inputVerticalDelta, _isSprinting);
            _zoomHandler.Tick(_isZooming);
        }
    }
}