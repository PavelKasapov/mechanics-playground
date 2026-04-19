using MechanicsPlayground.Core;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MechanicsPlayground.Tests.Core
{
    [TestFixture]
    public class FeatureModulesPanelPresenterTests
    {
        private FeatureModulesPanelPresenter _presenter;
        private FeatureManager _featureManagerMock;
        private Transform _panelViewMock;
        private SimpleMonobehaviourFactory<FeatureModuleGroup> _groupFactoryMock;
        private SimpleMonobehaviourFactory<FeatureButton> _buttonFactoryMock;
        private FeatureModuleGroup _groupMock;
        private List<FeatureButton> _buttonMocks;

        [SetUp]
        public void Setup()
        {
            _featureManagerMock = Substitute.For<FeatureManager>();
            _panelViewMock = Substitute.For<Transform>();
            _groupFactoryMock = Substitute.For<SimpleMonobehaviourFactory<FeatureModuleGroup>>();
            _buttonFactoryMock = Substitute.For<SimpleMonobehaviourFactory<FeatureButton>>();

            _groupMock = Substitute.For<FeatureModuleGroup>();
            _buttonMocks = new List<FeatureButton>() { Substitute.For<FeatureButton>(), Substitute.For<FeatureButton>() };

            _groupFactoryMock.Create().Returns(_groupMock);
            _buttonFactoryMock.Create().Returns(_buttonMocks[0], _buttonMocks[1]);

            /*_presenter = new FeatureModulesPanelPresenter(
                _featureManagerMock,
                _panelViewMock,
                _groupFactoryMock,
                _buttonFactoryMock
            );*/
        }

        [Test]
        public void Initialize_CreatesGroupAndButtonsForEachModule()
        {
            _presenter.Initialize();

            _groupFactoryMock.Received(1).Create();
            _buttonFactoryMock.Received(2).Create();

            _groupMock.Received(1).Init(FeatureCategory.Camera.ToString(), Arg.Is<List<FeatureButton>>(list => list.SequenceEqual(_buttonMocks)));
            _buttonMocks[0].Received(1).Init("Free 3D Camera", Arg.Any<Action>());
        }

        /*[Test]
        public void OnButtonClick_InvokesCorrectAction()
        {
            Action capturedAction = null;
            _buttonMocks[0].When(x => x.Init(Arg.Any<string>(), Arg.Any<Action>())).Do(call => capturedAction = call.Arg<Action>());

            _presenter.Initialize();

            capturedAction?.Invoke();
            _featureManagerMock.Received(1).ActivateModule<MechanicsPlayground.Free3DCamera.Scope>(FeatureCategory.Camera);
        }*/
    }
}