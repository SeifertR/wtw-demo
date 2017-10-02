using System;
using System.Reflection;

namespace OptimizationTests.IoC.RegisteredTypes
{
    public class CachedRegisteredType
    {
        public Type ConcreteType { get; }
        public LifeCycle LifeCycle { get; }
        public object Instance { get; private set; }
        private readonly object locker = new object();
        public ConstructorInfo Ctor { get; }
        public ParameterInfo[] Parameters { get; }

        public CachedRegisteredType(Type concreteType, LifeCycle lifeCycle, ConstructorInfo ctor, ParameterInfo[] parameters)
        {
            ConcreteType = concreteType ??
                           throw new ArgumentNullException(nameof(concreteType), "You must supply a concrete type.");

            LifeCycle = lifeCycle;
            Ctor = ctor;
            Parameters = parameters;
        }

        /// <summary>
        /// Create an instance of ConcreteType. If the life cycle is singleton, store
        /// the instance.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>An instance of ConcreteType.</returns>
        public object CreateInstance(params object[] parameters)
        {
            return LifeCycle == LifeCycle.Transient ? CreateTransient(parameters) : CreateSingleton(parameters);
        }

        private object CreateTransient(params object[] parameters)
        {
            return Ctor.Invoke(parameters);
        }

        private object CreateSingleton(params object[] parameters)
        {
            // Simple lock to prevent 2 different threads from
            // trying to store a singleton at the same time
            lock (locker)
            {
                // If Instance isn't null then another thread
                // already stored an instance.
                if (Instance == null)
                    Instance = Ctor.Invoke(parameters);
            }

            return Instance;
        }
    }
}
