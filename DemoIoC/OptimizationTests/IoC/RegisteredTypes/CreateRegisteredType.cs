using System;
using System.Reflection;

namespace OptimizationTests
{
    public class CreateRegisteredType
    {
        public Type ConcreteType { get; }
        public LifeCycle LifeCycle { get; }
        public object Instance { get; private set; }
        private readonly object locker = new object();

        public CreateRegisteredType(Type concreteType, LifeCycle lifeCycle)
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
        public object CreateInstance(params object[] parameters)
        {
            return LifeCycle == LifeCycle.Transient ? CreateTransient(parameters) : CreateSingleton(parameters);
        }

        private object CreateTransient(params object[] parameters)
        {
            return Activator.CreateInstance(ConcreteType, parameters);
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
                    Instance = Activator.CreateInstance(ConcreteType, parameters);
            }

            return Instance;
        }
    }
}
