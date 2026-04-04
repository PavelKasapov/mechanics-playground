using VContainer.Unity;
using MechanicsPlayground.Core;
using NUnit.Framework;
using NSubstitute;
using MechanicsPlayground.FeatureManagement;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class FeatureManagerTests
    {

        private LifetimeScope _parentScope;
        private FeatureManager _manager;

        [SetUp]
        public void Setup()
        {
            _parentScope = Substitute.For<LifetimeScope>();
            _manager = new FeatureManager(_parentScope);
        }

        [Test]
        public void ActivateModule_WhenNoActive_ShouldCreateNewScope()
        {
            var mockChild = Substitute.For<LifetimeScope>();

            _parentScope.CreateChild<TestScope1>().Returns(mockChild);

            _manager.ActivateModule<TestScope1>(FeatureCategory.Camera);

            _parentScope.Received(1).CreateChild<TestScope1>();
            mockChild.DidNotReceive().Dispose();
        }

        [Test]
        public void ActivateModule_WhenDifferentTypeActive_ShouldDisposeOldAndCreateNew()
        {
            var mockChild1 = Substitute.For<LifetimeScope>();
            var mockChild2 = Substitute.For<LifetimeScope>();

            _parentScope.CreateChild<TestScope1>().Returns(mockChild1);
            _parentScope.CreateChild<TestScope2>().Returns(mockChild2);

            _manager.ActivateModule<TestScope1>(FeatureCategory.Camera);
            _manager.ActivateModule<TestScope2>(FeatureCategory.Camera);

            mockChild1.Received(1).Dispose();
            _parentScope.Received(1).CreateChild<TestScope2>();
        }

        [Test]
        public void ActivateModule_WhenSameTypeActive_ShouldDoNothing()
        {
            var mockChild = Substitute.For<LifetimeScope>();

            _parentScope.CreateChild<TestScope1>().Returns(mockChild);

            _manager.ActivateModule<TestScope1>(FeatureCategory.Camera);
            _manager.ActivateModule<TestScope1>(FeatureCategory.Camera);

            _parentScope.Received(1).CreateChild<TestScope1>();
            mockChild.DidNotReceive().Dispose();
        }

    }

    public class TestScope1 : LifetimeScope { }
    public class TestScope2 : LifetimeScope { }
}