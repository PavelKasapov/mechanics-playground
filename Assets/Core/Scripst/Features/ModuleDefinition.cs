using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    [CreateAssetMenu(fileName = "ModuleDefinition", menuName = "Mechanics Playground/Module Definition")]
    public class ModuleDefinition : ScriptableObject
    {
        [SerializeField] private LifetimeScope _scope;
        [SerializeField] private string _displayName;
        [SerializeField] private FeatureCategory _category;
        [SerializeField] private GameObject _helpPrefab;

        public LifetimeScope ScopePrefab => _scope;
        public string DisplayName => _displayName;
        public FeatureCategory FeatureCategory => _category;
        public GameObject HelpPrefab => _helpPrefab;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_displayName))
                Debug.LogError($"ModuleDefinition {name} has no display name", this);

            if (_helpPrefab == null)
                Debug.LogError($"ModuleDefinition {name} has no help prefab", this);

            if (_scope == null)
                Debug.LogError($"ModuleDefinition {name} has no scope prefab", this);
        }
    }
}