using MechanicsPlayground.Core;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class FeatureModuleGroupTests : MonoBehaviour
    {
        [Test]
        public void Init_SetsTitleTextAndParentForButtons()
        {
            var go = new GameObject("FeatureModuleGroup");
            var featureModuleGroup = go.AddComponent<FeatureModuleGroup>();

            var textGo = new GameObject("TitleTextMesh");
            textGo.transform.SetParent(go.transform);
            var textMesh = textGo.AddComponent<TextMeshProUGUI>();
            featureModuleGroup.SetPrivateField("_titleTextMesh", textMesh);

            var settingsPlacementGo = new GameObject("SettingsPlacement");
            settingsPlacementGo.transform.SetParent(go.transform);
            featureModuleGroup.SetPrivateField("_settingsPlacement", settingsPlacementGo.transform);

            var featureButtons = new List<FeatureButton>();
            for (int i = 0; i < 3; i++) 
            {
                var buttonGo = new GameObject("FeatureButton");
                buttonGo.transform.SetParent(go.transform); //Only to be sure they were destroyed after test fail.
                var button = buttonGo.AddComponent<FeatureButton>();
                featureButtons.Add(button);
            }

            featureModuleGroup.Init("Test FeatureModuleGroup", featureButtons);

            Assert.AreEqual("Test FeatureModuleGroup", textMesh.text);

            var resultButtons = settingsPlacementGo.GetComponentsInChildren<FeatureButton>();
            Assert.AreEqual(3, resultButtons.Length);
            foreach (var button in resultButtons)
            {
                Assert.AreEqual(settingsPlacementGo.transform, button.transform.parent);
            }

            Object.Destroy(go);
        }
    }
}