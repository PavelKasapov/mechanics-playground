using MechanicsPlayground.Core;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class FeatureButtonTests
    {
        [Test]
        public void Init_SetsButtonTextAndInvokeActionOnClick()
        {
            var go = new GameObject("FeatureButton");
            var featureButton = go.AddComponent<FeatureButton>();

            var uiButton = go.AddComponent<Button>();
            featureButton.SetPrivateField("_selfButton", uiButton);

            var textGo = new GameObject("Text");
            textGo.transform.SetParent(go.transform);
            var text = textGo.AddComponent<TextMeshProUGUI>();
            featureButton.SetPrivateField("_textMesh", text);

            bool actionInvoked = false;
            string testName = "Test Button";

            featureButton.Init(testName, () => actionInvoked = true);
            Assert.AreEqual(testName, text.text);

            uiButton.onClick.Invoke();
            Assert.IsTrue(actionInvoked, "Action was not invoked on click");

            Object.Destroy(go);
        }
    }
}