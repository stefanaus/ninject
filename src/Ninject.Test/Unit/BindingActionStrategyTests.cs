﻿

namespace Ninject.Tests.Unit.BindingActionStrategyTests
{
    using System;
    using Moq;
    using Ninject.Activation;
    using Ninject.Activation.Strategies;
    using Ninject.Planning.Bindings;
#if SILVERLIGHT
#if SILVERLIGHT_MSTEST
    using MsTest.Should;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
#else
    using UnitDriven;
    using UnitDriven.Should;
    using Fact = UnitDriven.TestMethodAttribute;
#endif
#else
    using Ninject.Tests.MSTestAttributes;
    using Xunit;
    using Xunit.Should;
#endif

    public class BindingActionStrategyContext
    {
        protected BindingActionStrategy strategy;
        protected Mock<IContext> contextMock;
        protected Mock<IBinding> bindingMock;

        public BindingActionStrategyContext()
        {
            this.SetUp();
        }

        [TestInitialize]
        public void SetUp()
        {
            this.contextMock = new Mock<IContext>();
            this.bindingMock = new Mock<IBinding>();
            this.strategy = new BindingActionStrategy();
        }
    }

    [TestClass]
    public class WhenActivateIsCalled : BindingActionStrategyContext
    {
        [Fact]
        public void StrategyInvokesActivationActionsDefinedInBinding()
        {
            bool action1WasCalled = false;
            bool action2WasCalled = false;

            Action<IContext, object> action1 = (ctx, c) => action1WasCalled = ctx == contextMock.Object;
            Action<IContext, object> action2 = (ctx, c) => action2WasCalled = ctx == contextMock.Object;
            var actions = new[] { action1, action2 };

            contextMock.SetupGet(x => x.Binding).Returns(bindingMock.Object);
            bindingMock.SetupGet(x => x.ActivationActions).Returns(actions);
            strategy.Activate(contextMock.Object, new InstanceReference());

            action1WasCalled.ShouldBeTrue();
            action2WasCalled.ShouldBeTrue();
        }
    }

    [TestClass]
    public class WhenDeactivateIsCalled : BindingActionStrategyContext
    {
        [Fact]
        public void StrategyInvokesDeactivationActionsDefinedInBinding()
        {
            bool action1WasCalled = false;
            bool action2WasCalled = false;

            Action<IContext, object> action1 = (ctx, c) => action1WasCalled = ctx == contextMock.Object;
            Action<IContext, object> action2 = (ctx, c) => action2WasCalled = ctx == contextMock.Object;
            var actions = new[] { action1, action2 };

            contextMock.SetupGet(x => x.Binding).Returns(bindingMock.Object);
            bindingMock.SetupGet(x => x.DeactivationActions).Returns(actions);
            strategy.Deactivate(contextMock.Object, new InstanceReference());

            action1WasCalled.ShouldBeTrue();
            action2WasCalled.ShouldBeTrue();
        }
    }
}