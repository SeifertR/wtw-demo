using System;
using Xunit;

namespace DemoIoC.UnitTests
{
    public class RegisteredTypeTests
    {
        private class MockClass
        {
        }

        [Fact]
        public void NullConcreteTypeWillThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RegisteredType(null, LifeCycle.Singleton));
        }

        [Fact]
        public void SingletonsAreStored()
        {
            var registeredType = new RegisteredType(typeof(MockClass), LifeCycle.Singleton);
            var myClass = registeredType.CreateInstance();
            Assert.Equal(myClass, registeredType.Instance);
        }

        [Fact]
        public void TransientsAreNotStored()
        {
            var registeredType = new RegisteredType(typeof(MockClass), LifeCycle.Transient);
            var myClass = registeredType.CreateInstance();
            Assert.Null(registeredType.Instance);
        }
    }
}
