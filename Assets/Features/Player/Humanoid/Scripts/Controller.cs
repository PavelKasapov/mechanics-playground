using MechanicsPlayground.Core;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.HumanoidPlayer
{
    public class Controller : IInitializable, ITickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly MovementHandler _movementHandler;
        private readonly SettingsRegistry _settingsRegistry;
        private readonly IEnumerable<ISettings> _settings;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _inputMoveDelta;
        private bool _isSprinting;

        public Controller (
            InputAdapter inputAdapter, 
            MovementHandler movementHandler, 
            SettingsRegistry settingsRegistry,
            IEnumerable<ISettings> settings)
        {
            _inputAdapter = inputAdapter;
            _movementHandler = movementHandler;
            _settingsRegistry = settingsRegistry;
            _settings = settings;
        }

        public void Dispose() =>_disposables.Dispose();

        public void Initialize()
        {
            _inputAdapter.Move.Subscribe(moveDelta => { _inputMoveDelta = moveDelta; }).AddTo(_disposables);
            _inputAdapter.Sprint.Subscribe(isSprinting => { _isSprinting = isSprinting; }).AddTo(_disposables);
            _inputAdapter.Jump.Subscribe(zoomingDelta => { }).AddTo(_disposables);

            _settingsRegistry.RegisterModule("Humanoid Player", _settings.SelectMany(s => s.GetDescriptors()).ToList()).AddTo(_disposables);
        }

        public void Tick()
        {
            _movementHandler.Tick(_inputMoveDelta, _isSprinting);
        }
    }
}