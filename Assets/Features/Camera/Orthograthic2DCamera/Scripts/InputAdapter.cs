using R3;
using ReactiveInputSystem;
using System;
using UnityEngine;

namespace MechanicsPlayground.Orthographic2DCamera
{
    public class InputAdapter : IDisposable
    {
        private readonly Orthographic2DCameraInputActions _inputActions;
        public Observable<Vector2> Move => _inputActions.Camera.Move.PerformedAsObservable()
                .Merge(_inputActions.Camera.Move.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<Vector2> MousePosition => _inputActions.Camera.MousePosition.PerformedAsObservable()
                .Merge(_inputActions.Camera.MousePosition.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<float> Zoom => _inputActions.Camera.Zoom.PerformedAsObservable()
                .Merge(_inputActions.Camera.Zoom.CanceledAsObservable()).Select(ctx => ctx.ReadValue<float>());
        public Observable<bool> Sprint => _inputActions.Camera.Sprint.PerformedAsObservable().Select(_ => true)
                .Merge(_inputActions.Camera.Sprint.CanceledAsObservable().Select(_ => false));

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
