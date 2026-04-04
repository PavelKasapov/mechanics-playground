using System;
using UnityEngine;
using VContainer;
using VContainer.Diagnostics;

namespace MechanicsPlayground.Tests.Core
{
    public class TestResolver : IObjectResolver
    {
        public object ApplicationOrigin => throw new NotImplementedException();

        public DiagnosticsCollector Diagnostics { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public T Instantiate<T>(T prefab) where T : UnityEngine.Object => UnityEngine.Object.Instantiate(prefab);
        public T Instantiate<T>(T prefab, Transform parent) where T : UnityEngine.Object => UnityEngine.Object.Instantiate(prefab, parent);

        public IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Inject(object instance)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type, object key = null)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Registration registration)
        {
            throw new NotImplementedException();
        }

        public bool TryGetRegistration(Type type, out Registration registration, object key = null)
        {
            throw new NotImplementedException();
        }

        public bool TryResolve(Type type, out object resolved, object key = null)
        {
            throw new NotImplementedException();
        }
    }
}