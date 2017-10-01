using DemoIoC.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoIoC
{
    public class Container
    {
        private readonly Dictionary<Type, RegisteredType> objectMap = new Dictionary<Type, RegisteredType>();
        private readonly object locker = new object();

        /// <summary>
        /// Registers that a type of TConcrete will be returned when a type
        /// of TResolved is requested.
        /// </summary>
        /// <typeparam name="TResolved">The type that can be used to return an instance of TConcrete.</typeparam>
        /// <typeparam name="TConcrete">The type that is returned when TResolved is requested.</typeparam>
        /// <param name="lifeCycle">Indicates whether the instance of TConcrete should
        /// be unique (Transient) or shared (Singleton).</param>
        public void Register<TResolved, TConcrete>(LifeCycle lifeCycle = LifeCycle.Transient)
        {
            // Simple lock to prevent multiple threads from registering types
            // at the same time.
            lock (locker)
            {
                var resolvedType = typeof(TResolved);

                if (objectMap.ContainsKey(resolvedType))
                    throw new TypeAlreadyRegisteredException($"{resolvedType.FullName} has already been registered.");

                var registeredType = new RegisteredType(typeof(TConcrete), lifeCycle);
                objectMap.Add(resolvedType, registeredType);
            }
        }

        /// <summary>
        /// Return an instance of type T.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <returns>Returns an instance of type T, assuming that type T has
        /// been registered.</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type typeToResolve)
        {
            if (!objectMap.ContainsKey(typeToResolve))
                throw new TypeNotRegisteredException($"{typeToResolve.FullName} has not been registered.");

            var registeredType = objectMap[typeToResolve];

            // If we have already stored the requested type, return it.
            if (registeredType.Instance != null)
                return registeredType.Instance;

            // Get the requested type's constructor and see if it requires parameters
            var ctor = registeredType.ConcreteType.GetConstructors().First();
            var ctorParameters = ctor.GetParameters();

            // If no parameters are needed, create the requested object and return it
            if (ctorParameters.Length == 0)
                return registeredType.CreateInstance();

            // Get a list of the required parameters, recurssing as needed
            IList<object> parameters = ctorParameters.Select(paramToResolve => Resolve(paramToResolve.ParameterType)).ToList();

            // Return the requested type
            return registeredType.CreateInstance(parameters.ToArray());
        }
    }
}
