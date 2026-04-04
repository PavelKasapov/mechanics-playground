using MechanicsPlayground.Core;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class ModuleSettingsGroupTests
    {

        [Test]
        public void Constructor_AssignsProrertiesCorrectly()
        {
            var settings = new List<ISettingsDescriptor> { Substitute.For<ISettingsDescriptor>(), Substitute.For<ISettingsDescriptor>() };
            var group = new ModuleSettingsGroup("Test Group", settings);
            Assert.AreEqual("Test Group", group.ModuleName);
            Assert.AreSame(settings, group.Settings);
            Assert.AreSame(settings, group.GetDescriptors());
        }
    }
}
