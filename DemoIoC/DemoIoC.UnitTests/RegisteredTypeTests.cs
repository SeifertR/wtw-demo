using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace DemoIoC.UnitTests
{
    public class RegisteredTypeTests
    {
        private class MockClass
        {
        }

        private ConstructorInfo Ctor => typeof(MockClass).GetConstructors().First();
        private ParameterInfo[] ctorParams = new ParameterInfo[0];

        [Fact]
        public void NullConcreteTypeWillThrow()
        {
            var rtc = typeof(RegisteredType).GetConstructors().First();
            var paramName = rtc.GetParameters()[0].Name;

            var ex = Assert.Throws<ArgumentNullException>(() =>
                new RegisteredType(null, LifeCycle.Singleton, Ctor, ctorParams));
            Assert.Equal(paramName, ex.ParamName);
        }

        [Fact]
        public void NullCtorWillThrow()
        {
            var rtc = typeof(RegisteredType).GetConstructors().First();
            var paramName = rtc.GetParameters()[2].Name;

            var ex = Assert.Throws<ArgumentNullException>(() =>
                new RegisteredType(typeof(MockClass), LifeCycle.Singleton, null, ctorParams));
            Assert.Equal(paramName, ex.ParamName);
        }

        [Fact]
        public void NullCtorParamsWillThrow()
        {
            var rtc = typeof(RegisteredType).GetConstructors().First();
            var paramName = rtc.GetParameters()[3].Name;

            var ex = Assert.Throws<ArgumentNullException>(() =>
                new RegisteredType(typeof(MockClass), LifeCycle.Singleton, Ctor, null));
            Assert.Equal(paramName, ex.ParamName);
        }

        [Fact]
        public void SingletonsAreStored()
        {
            var registeredType = new RegisteredType(typeof(MockClass), LifeCycle.Singleton, Ctor, ctorParams);
            var myClass = registeredType.CreateInstance();
            Assert.Equal(myClass, registeredType.Instance);
        }

        [Fact]
        public void TransientsAreNotStored()
        {
            var registeredType = new RegisteredType(typeof(MockClass), LifeCycle.Transient, Ctor, ctorParams);
            var myClass = registeredType.CreateInstance();
            Assert.Null(registeredType.Instance);
        }
    }
}
