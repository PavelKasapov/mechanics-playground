using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MechanicsPlayground.Core
{
    public class FeatureModuleGroup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleTextMesh;
        [SerializeField] private Transform _settingsPlacement;

        public void Init(string moduleName, List<FeatureButton> featureButtons)
        {
            _titleTextMesh.text = moduleName;
            featureButtons.ForEach(button => button.transform.SetParent(_settingsPlacement));
        }
    }
}