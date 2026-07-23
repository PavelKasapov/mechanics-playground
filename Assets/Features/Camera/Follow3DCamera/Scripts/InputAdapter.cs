using R3;
using ReactiveInputSystem;
using System;
using UnityEngine;

namespace MechanicsPlayground.Follow3DCamera
{
    public class InputAdapter : IDisposable
    {
        private readonly Follow3DCameraInputActions _inputActions;
        public Observable<Vector2> Look => _inputActions.Camera.Look.PerformedAsObservable()
                .Merge(_inputActions.Camera.Look.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());

        public InputAdapter()
        {
            _inputActions = new();
            _inputActions.Camera.Enable();
        }

        public void Dispose()
        {
            _inputActions.Camera.Disable();
            _inputActions.Dispose();
        }
    }
}
