using MechanicsPlayground.Core;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.HumanoidMovement
{
    public class Controller : IInitializable, IFixedTickable, IDisposable
    {
        private readonly InputAdapter _inputAdapter;
        private readonly MovementHandler _movementHandler;
        private readonly SettingsRegistry _settingsRegistry;
        private readonly IEnumerable<ISettings> _settings;
        private readonly PlayerPivotHandler _playerTransformHandler;
        private readonly Transform _pivotTransform;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _inputMoveDelta;
        private bool _isSprinting;

        public Controller (
            InputAdapter inputAdapter, 
            MovementHandler movementHandler, 
            SettingsRegistry settingsRegistry,
            IEnumerable<ISettings> settings,
            PlayerPivotHandler playerTransformHandler,
            [Key("PivotTransform")] Transform pivotTransform)
        {
            _inputAdapter = inputAdapter;
            _movementHandler = movementHandler;
            _settingsRegistry = settingsRegistry;
            _settings = settings;
            _playerTransformHandler = playerTransformHandler;
            _pivotTransform = pivotTransform;
        }

        public void Dispose()
        {
            _playerTransformHandler.UnregisterPivot();
            _disposables.Dispose();
        }

        public void Initialize()
        {
            _inputAdapter.Move.Subscribe(moveDelta => { _inputMoveDelta = moveDelta; }).AddTo(_disposables);
            _inputAdapter.Sprint.Subscribe(isSprinting => { _isSprinting = isSprinting; }).AddTo(_disposables);
            _inputAdapter.Jump.Subscribe(zoomingDelta => { }).AddTo(_disposables);

            _inputAdapter.Jump.Subscribe(_ => _movementHandler.JumpAction()).AddTo(_disposables);

            _settingsRegistry.RegisterModule("Humanoid Player", _settings.SelectMany(s => s.GetDescriptors()).ToList()).AddTo(_disposables);

            _playerTransformHandler.RegisterPivot(_pivotTransform);
        }

        public void FixedTick()
        {
            _movementHandler.FixedTick(_inputMoveDelta, _isSprinting);
        }
    }
}