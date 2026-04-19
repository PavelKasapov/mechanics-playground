using UnityEngine;

namespace MechanicsPlayground.Core
{
    public class HelpWindow : MonoBehaviour
    {
        [SerializeField] private Transform _activeModulesPlaceholder;
        [SerializeField] private Transform _inactiveModulesPlaceholder;
        [SerializeField] private Transform _dynamicHelpArea;

        public Transform ActiveModulesPlaceholder => _activeModulesPlaceholder;
        public Transform InactiveModulesPlaceholder => _inactiveModulesPlaceholder;
        public Transform DynamicHelpArea => _dynamicHelpArea;
    }
}