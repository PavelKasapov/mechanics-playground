using R3;
using ReactiveInputSystem;
using System;
using UnityEngine;

namespace MechanicsPlayground.Camera3D
{
    public class InputAdapter : IDisposable
    {
        private readonly Camera3DInputActions _inputActions;
        public Observable<Vector2> Move => _inputActions.Camera3D.Move.PerformedAsObservable()
                .Merge(_inputActions.Camera3D.Move.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<Vector2> Look => _inputActions.Camera3D.Look.PerformedAsObservable()
                .Merge(_inputActions.Camera3D.Look.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<float> Zoom => _inputActions.Camera3D.Zoom.PerformedAsObservable()
                .Merge(_inputActions.Camera3D.Zoom.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<float>());

        public InputAdapter()
        {
            _inputActions = new();
            _inputActions.Camera3D.Enable();
        }

        public void Dispose()
        {
            _inputActions.Camera3D.Disable();
            _inputActions.Dispose();
        }
    }
}
