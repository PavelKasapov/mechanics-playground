using NUnit.Framework;
using MechanicsPlayground.Core;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class FloatSettingDescriptorTests
    {
        [Test]
        public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
        {
            var desc = new FloatSettingDescriptor("Test", 10f, 5f, 20f, 0.5f);
            Assert.AreEqual("Test", desc.DisplayName);
            Assert.AreEqual(10f, desc.Value);
            Assert.AreEqual(5f, desc.Min);
            Assert.AreEqual(20f, desc.Max);
            Assert.AreEqual(0.5f, desc.Step);
        }
    }
}