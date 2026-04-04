using MechanicsPlayground.Core;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MechanicsPlayground.Tests.Core
{
    public class SettingsControlModuleTests : MonoBehaviour
    {
        [Test]
        public void Init_SetsTitleTextAndParentForControls()
        {
            var go = new GameObject("SettingsControlModule");
            var settingsControlModule = go.AddComponent<SettingsControlModule>();

            var textGo = new GameObject("TitleTextMesh");
            textGo.transform.SetParent(go.transform);
            var textMesh = textGo.AddComponent<TextMeshProUGUI>();
            settingsControlModule.SetPrivateField("_titleTextMesh", textMesh);

            var settingsPlacementGo = new GameObject("SettingsPlacement");
            settingsPlacementGo.transform.SetParent(go.transform);
            settingsControlModule.SetPrivateField("_settingsPlacement", settingsPlacementGo.transform);

            var settingControls = new List<BaseSettingControl>();
            for (int i = 0; i < 3; i++)
            {
                var settingControlGo = new GameObject("SettingControl");
                settingControlGo.transform.SetParent(go.transform); //Only to be sure they were destroyed after test fail.
                var settingControl = settingControlGo.AddComponent<FloatSettingControl>();
                settingControls.Add(settingControl);
            }

            settingsControlModule.Init("Test SettingsControlModule", settingControls);

            Assert.AreEqual("Test SettingsControlModule", textMesh.text);

            var resultSettingControls = settingsPlacementGo.GetComponentsInChildren<BaseSettingControl>();
            Assert.AreEqual(3, resultSettingControls.Length);
            foreach (var settingControl in resultSettingControls)
            {
                Assert.AreEqual(settingsPlacementGo.transform, settingControl.transform.parent);
            }

            CollectionAssert.AreEqual(resultSettingControls, settingsControlModule.SettingControls);

            Object.Destroy(go);
        }
    }
}
