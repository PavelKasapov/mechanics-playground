using R3;
using ReactiveInputSystem;
using System;
using UnityEngine;

namespace MechanicsPlayground.HumanoidPlayer
{
    public class InputAdapter : IDisposable
    {
        private readonly HumanoidPlayerInputActions _inputActions;
        public Observable<Vector2> Move => _inputActions.PlayerMovement.Move.PerformedAsObservable()
                .Merge(_inputActions.PlayerMovement.Move.CanceledAsObservable())
                .Select(ctx => ctx.ReadValue<Vector2>());
        public Observable<bool> Sprint => _inputActions.PlayerMovement.Sprint.PerformedAsObservable().Select(_ => true)
                .Merge(_inputActions.PlayerMovement.Sprint.CanceledAsObservable().Select(_ => false));

        public Observable<Unit> Jump => _inputActions.PlayerMovement.Jump.PerformedAsObservable().AsUnitObservable();

        public InputAdapter()
        {
            _inputActions = new();
            _inputActions.PlayerMovement.Enable();
        }

        public void Dispose()
        {
            _inputActions.PlayerMovement.Disable();
            _inputActions.Dispose();
        }
    }
}
