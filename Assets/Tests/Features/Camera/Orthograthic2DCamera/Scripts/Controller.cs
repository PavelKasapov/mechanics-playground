using MechanicsPlayground.Core;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using System.Linq;

namespace MechanicsPlayground.Tests.Orthographic2DCamera
{
    /*public class Controller : IInitializable, ITickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly Camera _camera;
        private readonly MovementHandler _movementHandler;
        private readonly ZoomHandler _zoomHandler;
        private readonly SettingsRegistry _settingsRegistry;
        private readonly IEnumerable<ISettings> _settings;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _inputMoveDelta;
        private Vector2 _mousePosition;
        private bool _isSprinting;
        private float _inputZoomingDelta;

        public Controller (InputAdapter inputAdapter, Camera camera, MovementHandler movementHandler, ZoomHandler zoomHandler, SettingsRegistry settingsRegistry ,IEnumerable<ISettings> settings)
        {
            _inputAdapter = inputAdapter;
            _camera = camera;
            _movementHandler = movementHandler;
            _zoomHandler = zoomHandler;
            _settingsRegistry = settingsRegistry;
            _settings = settings;
        }

        public void Dispose() =>_disposables.Dispose();

        public void Initialize()
        {
            _inputAdapter.Move.Subscribe(moveDelta => { _inputMoveDelta = moveDelta; }).AddTo(_disposables);
            _inputAdapter.Sprint.Subscribe(isSprinting => { _isSprinting = isSprinting; }).AddTo(_disposables);
            _inputAdapter.Zoom.Subscribe(zoomingDelta => { _inputZoomingDelta = zoomingDelta; }).AddTo(_disposables);
            _inputAdapter.MousePosition.Subscribe(mousePosition => { _mousePosition = mousePosition; }).AddTo(_disposables);

            SetupStartCameraValues();

            _settingsRegistry.RegisterModule("Orthographic2DCamera", _settings.SelectMany(s => s.GetDescriptors()).ToList()).AddTo(_disposables);
        }

        public void Tick()
        {
            _movementHandler.Tick(_inputMoveDelta, _mousePosition, _isSprinting);
            _zoomHandler.Tick(_inputZoomingDelta);
        }

        private void SetupStartCameraValues()
        {
            _camera.transform.position = new(0, 10, 0);
            _camera.transform.rotation = Quaternion.Euler(new(90, 0, 0));
            _camera.orthographicSize = 30;
            _camera.orthographic = true;
        }
    }*/
}