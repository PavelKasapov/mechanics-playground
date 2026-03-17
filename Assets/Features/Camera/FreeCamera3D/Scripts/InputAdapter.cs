using R3;
using ReactiveInputSystem;
using System;
using UnityEngine;

namespace MechanicsPlayground.FreeCamera3D
{
    public class InputAdapter : IDisposable
    {
        private readonly FreeCamera3DInputActions _inputActions;
        public Observable<Vector2> Move => _inputActions.Camera.Move.PerformedAsObservable()
                .Merge(_inputActions.Camera.Move.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<Vector2> Look => _inputActions.Camera.Look.PerformedAsObservable()
                .Merge(_inputActions.Camera.Look.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<bool> Zoom => _inputActions.Camera.Zoom.PerformedAsObservable().Select(_ => true)
                .Merge(_inputActions.Camera.Zoom.CanceledAsObservable().Select(_ => false));
        public Observable<bool> Sprint => _inputActions.Camera.Sprint.PerformedAsObservable().Select(_ => true)
                .Merge(_inputActions.Camera.Sprint.CanceledAsObservable().Select(_ => false));
        public Observable<float> VerticalMovement => _inputActions.Camera.VerticalMovement.PerformedAsObservable()
                .Merge(_inputActions.Camera.VerticalMovement.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<float>());

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
