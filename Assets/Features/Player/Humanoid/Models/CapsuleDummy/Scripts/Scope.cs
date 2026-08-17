using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.CapsuleDummy
{
    public class Scope : LifetimeScope, IInitializable, IDisposable
    {
        [SerializeField] private LifetimeScope _movementScope;
        [SerializeField] private GroundedProvider _groundedController;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _pivot;
        [SerializeField] private Rigidbody _rigidbody;

        private LifetimeScope _childScope;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_player).Keyed("PlayerTransform");
            builder.RegisterInstance(_pivot).Keyed("PivotTransform");
            builder.RegisterInstance(_groundedController);
            builder.RegisterInstance(_rigidbody);

            builder.Register<IInitializable>(_ => this, Lifetime.Singleton);
            builder.Register<IDisposable>(_ => this, Lifetime.Singleton);
        }

        public void Initialize()
        {
            _childScope = this.CreateChildFromPrefab(_movementScope);
        }

        public void Dispose()
        {
            _childScope?.Dispose();
        }
    }
}