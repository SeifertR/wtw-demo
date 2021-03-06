﻿using System;
using System.Reflection;

namespace DemoIoC
{
    public class RegisteredType
    {
        public Type ConcreteType { get; }
        public LifeCycle LifeCycle { get; }
        public object Instance { get; private set; }
        public ConstructorInfo CtorInfo { get; }
        public ParameterInfo[] CtorParams { get; }

        private readonly object locker = new object();

        public RegisteredType(Type concreteType, LifeCycle lifeCycle, ConstructorInfo ctor, ParameterInfo[] parameters)
        {
            ConcreteType = concreteType ??
                           throw new ArgumentNullException(nameof(concreteType), "You must supply a concrete type.");

            LifeCycle = lifeCycle;

            CtorInfo = ctor ?? throw new ArgumentNullException(nameof(ctor), "You must supply a ConstructorInfo.");
            CtorParams = parameters ??
                         throw new ArgumentNullException(nameof(parameters), "You must supply a ParameterInfo[]");
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
            return CtorInfo.Invoke(parameters);
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
                    Instance = CtorInfo.Invoke(parameters);
            }

            return Instance;
        }
    }
}
