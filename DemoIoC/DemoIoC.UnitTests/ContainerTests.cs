using DemoIoC.Exceptions;
using Xunit;

namespace DemoIoC.UnitTests
{
    public class ContainerTests
    {
        #region Mock types

        private interface IMockType { }

        private interface IComplexMockType
        {
            IMockType MockMember { get; }
        }

        private class MockType : IMockType { }

        private class ComplexMockType : IComplexMockType
        {
            public IMockType MockMember { get; }
            public ComplexMockType(IMockType mockType)
            {
                MockMember = mockType;
            }
        }

        #endregion Mock types

        [Fact]
        public void RegisteringAlreadyRegisteredTypeThrows()
        {
            var container = new Container();
            container.Register<MockType, MockType>();

            Assert.Throws<TypeAlreadyRegisteredException>(() => container.Register<MockType, MockType>());
        }

        [Fact]
        public void ResolvingUnregisteredTypeThrows()
        {
            var container = new Container();
            Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<MockType>());
        }

        [Fact]
        public void ResolveReturnsCorrectType()
        {
            var container = new Container();
            container.Register<IMockType, MockType>();
            var mockType = container.Resolve<IMockType>();
            Assert.True(mockType is IMockType);
        }

        [Fact]
        public void TransientsReturnUniqueInstances()
        {
            var container = new Container();
            container.Register<IMockType, MockType>(LifeCycle.Transient);
            var mockType1 = container.Resolve<IMockType>();
            var mockType2 = container.Resolve<IMockType>();
            Assert.NotEqual(mockType1, mockType2);
        }

        [Fact]
        public void RegisterDefaultsToTransient()
        {
            var container = new Container();
            container.Register<IMockType, MockType>();
            var mockType1 = container.Resolve<IMockType>();
            var mockType2 = container.Resolve<IMockType>();
            Assert.NotEqual(mockType1, mockType2);
        }

        [Fact]
        public void SingletonsReturnSameInstance()
        {
            var container = new Container();
            container.Register<IMockType, MockType>(LifeCycle.Singleton);
            var mockType1 = container.Resolve<IMockType>();
            var mockType2 = container.Resolve<IMockType>();
            Assert.Equal(mockType1, mockType2);
        }

        [Fact]
        public void ComplexTypesWillResolve()
        {
            var container = new Container();
            container.Register<IComplexMockType, ComplexMockType>();
            container.Register<IMockType, MockType>();
            var complexType = container.Resolve<IComplexMockType>();
            Assert.True(complexType is IComplexMockType);
            Assert.True(complexType.MockMember is IMockType);
        }
    }
}
