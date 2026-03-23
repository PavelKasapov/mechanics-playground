using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MechanicsPlayground.Core
{
    public class SettingsControlModule : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleTextMesh;
        [SerializeField] private Transform _settingsPlacement;

        public List<BaseSettingControl> SettingControls { get; private set; }

        public void Init(string moduleName, List<BaseSettingControl> settingControls)
        {
            _titleTextMesh.text = moduleName;
            SettingControls = settingControls;
            SettingControls.ForEach(control => control.transform.SetParent(_settingsPlacement));
        }
    }
}
