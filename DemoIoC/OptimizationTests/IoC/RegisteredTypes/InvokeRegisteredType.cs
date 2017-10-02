using System;
using System.Reflection;

namespace OptimizationTests.IoC.RegisteredTypes
{
    public class InvokeRegisteredType
    {
        public Type ConcreteType { get; }
        public LifeCycle LifeCycle { get; }
        public object Instance { get; private set; }
        private readonly object locker = new object();

        public InvokeRegisteredType(Type concreteType, LifeCycle lifeCycle)
        {
            ConcreteType = concreteType ??
                           throw new ArgumentNullException(nameof(concreteType), "You must supply a concrete type.");

            LifeCycle = lifeCycle;
        }

        /// <summary>
        /// Create an instance of ConcreteType. If the life cycle is singleton, store
        /// the instance.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>An instance of ConcreteType.</returns>
        public object CreateInstance(ConstructorInfo ctor, params object[] parameters)
        {
            return LifeCycle == LifeCycle.Transient ? CreateTransient(ctor, parameters) : CreateSingleton(ctor, parameters);
        }

        private object CreateTransient(ConstructorInfo ctor, params object[] parameters)
        {
            return ctor.Invoke(parameters);
        }

        private object CreateSingleton(ConstructorInfo ctor, params object[] parameters)
        {
            // Simple lock to prevent 2 different threads from
            // trying to store a singleton at the same time
            lock (locker)
            {
                // If Instance isn't null then another thread
                // already stored an instance.
                if (Instance == null)
                    Instance = ctor.Invoke(parameters);
            }

            return Instance;
        }
    }
}
