using MechanicsPlayground.Core;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.Follow3DCamera
{
    public class Controller : IInitializable, ITickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly RotationHandler _rotationHandler;
        private readonly SettingsRegistry _settingsRegistry;
        private readonly IEnumerable<ISettings> _settings;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _inputLookDelta;
        private bool _isCursorVisiblile;

        public Controller(
            InputAdapter inputAdapter,
            RotationHandler rotationHandler,
            SettingsRegistry settingsRegistry,
            IEnumerable<ISettings> settings)
        {
            _inputAdapter = inputAdapter;
            _rotationHandler = rotationHandler;
            _settingsRegistry = settingsRegistry;
            _settings = settings;
        }

        public void Dispose() => _disposables.Dispose();

        public void Initialize()
        {
            _inputAdapter.Look.Subscribe(lookDelta => { _inputLookDelta = lookDelta; }).AddTo(_disposables);

            _settingsRegistry.RegisterModule("Follow3DCamera", _settings.SelectMany(s => s.GetDescriptors()).ToList()).AddTo(_disposables);
        }

        public void Tick()
        {
            _rotationHandler.Tick(_inputLookDelta, _isCursorVisiblile);
        }
    }
}