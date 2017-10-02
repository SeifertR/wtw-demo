using System;

namespace OptimizationTests
{
    public class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException(string msg) : base(msg)
        {
        }
    }
}
