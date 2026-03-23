using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class SimpleMonobehaviourFactory<T> where T : MonoBehaviour
    {
        private readonly IObjectResolver _resolver;
        private readonly T _prefab;
        public SimpleMonobehaviourFactory(IObjectResolver resolver, T prefab)
        {
            _resolver = resolver;
            _prefab = prefab;
        }

        public T Create(Transform transform = null)
        {
            return transform != null 
                ? _resolver.Instantiate(_prefab, transform) 
                : _resolver.Instantiate(_prefab);
        }
    }
}
