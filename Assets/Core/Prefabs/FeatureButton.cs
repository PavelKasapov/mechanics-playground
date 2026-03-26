using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicsPlayground.Core
{
    public class FeatureButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Button _selfButton;

        public void Init(string moduleName, Action clickAction)
        {
            _textMesh.text = moduleName;
            _selfButton.onClick.RemoveAllListeners();
            _selfButton.onClick.AddListener(() => clickAction?.Invoke());
        }
    }
}